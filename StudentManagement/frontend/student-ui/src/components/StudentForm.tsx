import { useState } from "react";
import type { Student, StudentRequest } from "../models/Student";

interface Props {
    student?: Student;
    onSave: (student: StudentRequest) => void;
    onCancel: () => void;
}

export default function StudentForm({ student, onSave, onCancel }: Props) {
    const [form, setForm] = useState<StudentRequest>({
        firstName: student?.firstName ?? "",
        lastName: student?.lastName ?? "",
        email: student?.email ?? "",
        phone: student?.phone ?? "",
        dateOfBirth: student?.dateOfBirth ?? "",
        gender: student?.gender ?? "",
        course: student?.course ?? "",
        department: student?.department ?? "",
        address: student?.address ?? ""
    });

    const change = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const submit = (e: React.FormEvent) => {
        e.preventDefault();
        onSave(form);
    };

    return (
        <div className="form">
            <h2>{student ? "Edit Student" : "Add Student"}</h2>
            <form onSubmit={submit}>
                <input name="firstName" placeholder="First Name" value={form.firstName} onChange={change} required />
                <input name="lastName" placeholder="Last Name" value={form.lastName} onChange={change} required />
                <input name="email" type="email" placeholder="Email" value={form.email} onChange={change} required />
                <input name="phone" placeholder="Phone" value={form.phone} onChange={change} />
                <input name="dateOfBirth" type="date" value={form.dateOfBirth} onChange={change} />
                <select name="gender" value={form.gender} onChange={change}>
                    <option value="">Select Gender</option>
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                </select>
                <input name="course" placeholder="Course" value={form.course} onChange={change} />
                <input name="department" placeholder="Department" value={form.department} onChange={change} />
                <textarea name="address" placeholder="Address" value={form.address} onChange={change} />
                <button type="submit">Save</button>
                <button type="button" onClick={onCancel}>Cancel</button>
            </form>
        </div>
    );
}
