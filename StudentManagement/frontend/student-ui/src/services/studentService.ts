import axios from "axios";
import type { Student, StudentRequest, PagedResult } from "../models/Student";

const api = axios.create({
    baseURL: "http://localhost:5000/api"
});

export const getStudents = async (search: string, pageNumber: number) => {
    const response = await api.get<PagedResult<Student>>("/students", {
        params: { search, pageNumber, pageSize: 10 }
    });
    return response.data;
};

export const createStudent = async (student: StudentRequest) => {
    const response = await api.post<Student>("/students", student);
    return response.data;
};

export const updateStudent = async (id: number, student: StudentRequest) => {
    await api.put(`/students/${id}`, student);
};

export const deleteStudent = async (id: number) => {
    await api.delete(`/students/${id}`);
};
