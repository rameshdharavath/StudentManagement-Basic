import { useEffect, useState } from "react";
import StudentTable from "./components/StudentTable";
import StudentForm from "./components/StudentForm";
import type { Student, StudentRequest } from "./models/Student";
import { getStudents, createStudent, updateStudent, deleteStudent } from "./services/studentService";
import "./App.css";

function App() {
    const [students, setStudents] = useState<Student[]>([]);
    const [search, setSearch] = useState("");
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [showForm, setShowForm] = useState(false);
    const [selectedStudent, setSelectedStudent] = useState<Student | undefined>();

    const loadStudents = async () => {
        try {
            const result = await getStudents(search, page);
            setStudents(result.items);
            setTotalPages(result.totalPages);
        } catch (error) {
            console.error(error);
            alert("Unable to load students.");
        }
    };

    useEffect(() => {
        loadStudents();
    }, [page, search]);

    const saveStudent = async (student: StudentRequest) => {
        try {
            if (selectedStudent) {
                await updateStudent(selectedStudent.id, student);
            } else {
                await createStudent(student);
            }
            setShowForm(false);
            setSelectedStudent(undefined);
            await loadStudents();
        } catch (error) {
            console.error(error);
            alert("Unable to save student.");
        }
    };

    const removeStudent = async (id: number) => {
        if (!confirm("Delete this student?")) return;

        try {
            await deleteStudent(id);
            await loadStudents();
        } catch (error) {
            console.error(error);
            alert("Unable to delete student.");
        }
    };

    return (
        <div className="container">
            <h1>Student Management</h1>

            {!showForm && (
                <>
                    <div className="toolbar">
                        <input
                            placeholder="Search students..."
                            value={search}
                            onChange={e => {
                                setSearch(e.target.value);
                                setPage(1);
                            }}
                        />
                        <button onClick={() => {
                            setSelectedStudent(undefined);
                            setShowForm(true);
                        }}>
                            + Add Student
                        </button>
                    </div>

                    <StudentTable
                        students={students}
                        onEdit={student => {
                            setSelectedStudent(student);
                            setShowForm(true);
                        }}
                        onDelete={removeStudent}
                    />

                    <div className="pagination">
                        <button disabled={page === 1} onClick={() => setPage(page - 1)}>
                            Previous
                        </button>
                        <span>Page {page} of {totalPages}</span>
                        <button disabled={page >= totalPages} onClick={() => setPage(page + 1)}>
                            Next
                        </button>
                    </div>
                </>
            )}

            {showForm && (
                <StudentForm
                    student={selectedStudent}
                    onSave={saveStudent}
                    onCancel={() => {
                        setShowForm(false);
                        setSelectedStudent(undefined);
                    }}
                />
            )}
        </div>
    );
}

export default App;
