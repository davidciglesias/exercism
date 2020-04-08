using System.Collections.Generic;
using System.Linq;


public interface IGradeSchool
{
    public void Add(string student, int grade);
    public IEnumerable<string> Roster();
    public IEnumerable<string> Grade(int grade);
}

public class GradeSchool : IGradeSchool
{
    private readonly SortedDictionary<int, List<string>> roster = new SortedDictionary<int, List<string>>();

    public void Add(string student, int grade)
    {
        if (roster.ContainsKey(grade))
        {
            roster[grade].Add(student);
        }
        else roster.Add(grade, new List<string> { student });
    }

    public IEnumerable<string> Roster() => roster.Select(gradeStudents => { gradeStudents.Value.Sort(); return gradeStudents.Value; })
                                                 .SelectMany(x => x);

    public IEnumerable<string> Grade(int grade) => roster.Where((gradeStudent) => gradeStudent.Key == grade)
                                                         .SelectMany(studentGrade => studentGrade.Value)
                                                         .OrderBy(x => x);
}