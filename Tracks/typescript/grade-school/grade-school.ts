export default class GradeSchool {
    private students: Map<string, string[]>;
    constructor() {
        this.students = new Map();
    }
    studentRoster() {
        return new Map([...this.students.entries()].map(entry => [entry[0], [...entry[1]]]));
    }
    addStudent(name: string, grade: number) {
        const gradeStr = grade.toString();
        this.students.set(gradeStr, [...(this.students.get(gradeStr) || []), name].sort());
    }

    studentsInGrade(grade: number) {
        return [...(this.students.get(grade.toString()) || [])];
    }
}
