import type { Student } from "../models/Student";

interface Props {
    students: Student[];
    onEdit: (student: Student) => void;
    onDelete: (id: number) => void;
}

export default function StudentTable({ students, onEdit, onDelete }: Props) {
    return (
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>Course</th>
                    <th>Department</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {students.map(student => (
                    <tr key={student.id}>
                        <td>{student.id}</td>
                        <td>{student.firstName} {student.lastName}</td>
                        <td>{student.email}</td>
                        <td>{student.phone}</td>
                        <td>{student.course}</td>
                        <td>{student.department}</td>
                        <td>
                            <button onClick={() => onEdit(student)}>Edit</button>
                            <button onClick={() => onDelete(student.id)}>Delete</button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}
