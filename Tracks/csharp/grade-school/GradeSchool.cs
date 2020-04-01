using System.Collections.Generic;
using System.Linq;


public interface IGradeSchool
{
    public void Add(string student, int grade);
    public IEnumerable<string> Roster();
    public IEnumerable<string> Grade(int grade);
}

public class GradeSchool2 : IGradeSchool
{
    private readonly Dictionary<string, int> roster = new Dictionary<string, int>();

    public void Add(string student, int grade) => roster.Add(student, grade);

    public IEnumerable<string> Grade(int grade) => roster.Where(studentGrade => studentGrade.Value == grade)
                                                         .Select(studentGrade => studentGrade.Key)
                                                         .OrderBy(x => x)
                                                         .ToArray();

    public IEnumerable<string> Roster() => roster.OrderBy(studentGrade => studentGrade.Value)
                                                 .ThenBy(studentGrade => studentGrade.Key)
                                                 .Select(studentGrade => studentGrade.Key)
                                                 .ToArray();
}

public class GradeSchool : IGradeSchool
{
    private readonly SortedDictionary<int, List<string>> roster = new SortedDictionary<int, List<string>>();

    public void Add(string student, int grade)
    {
        if (roster.ContainsKey(grade))
        {
            roster[grade].Add(student);
            roster[grade].Sort();
        }
        else roster.Add(grade, new List<string> { student });
    }

    public IEnumerable<string> Roster() => roster.Values.SelectMany(x => x).ToArray();

    public IEnumerable<string> Grade(int grade) => roster.Where((gradeStudent) => gradeStudent.Key == grade).SelectMany(studentGrade => studentGrade.Value).ToArray();
}