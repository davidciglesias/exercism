using System;
using System.Collections.Generic;
using System.Linq;

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public Tree() { }

    public Tree(TreeBuildingRecord record)
    {
        Id = record.RecordId;
        ParentId = record.ParentId;
    }

    public int Id { get; set; }
    public int ParentId { get; set; }

    public List<Tree> Children { get; set; } = new List<Tree>();

    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    private static bool ValidateCollection(this IEnumerable<TreeBuildingRecord> collection) => collection.All(ValidateRecord) &&
        collection.Count(record => record.RecordId == 0 && record.ParentId == 0) == 1 && collection.Count() == (collection.Max(record => record.RecordId) + 1);

    private static bool ValidateRecord(this TreeBuildingRecord record) => record.RecordId >= 0 &&
        record.RecordId == 0 && record.ParentId == 0 || record.RecordId != 0 && record.ParentId < record.RecordId;

    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        if (!records.ValidateCollection())
        {
            throw new ArgumentException();
        }

        var result = new Tree { Id = 0, ParentId = 0 };
        var treeByParentId = new Dictionary<int, Tree> { { 0, result } };

        Tree RetrieveCurrentTreeAsParent(TreeBuildingRecord record)
        {
            if (treeByParentId.TryGetValue(record.RecordId, out Tree thisTree))
            {
                thisTree.Id = record.RecordId;
            }
            else
            {
                thisTree = new Tree(record);
            }
            return thisTree;
        }

        void InsertAsChildInParent(TreeBuildingRecord record)
        {
            Tree tree = RetrieveCurrentTreeAsParent(record);
            if (treeByParentId.TryGetValue(record.ParentId, out Tree parent))
            {
                parent.Children.Insert(0, tree);
            }
            else
            {
                treeByParentId.Add(record.ParentId, new Tree { Id = record.ParentId, Children = new List<Tree> { tree } });
            }
        }

        records.Where(record => record.RecordId != 0).OrderByDescending(record => record.RecordId).ToList().ForEach(InsertAsChildInParent);

        return result;
    }
}