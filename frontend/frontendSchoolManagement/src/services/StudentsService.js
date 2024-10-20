import api from "./ApiSettings";

export async function GetAllStudents() {
    const request = await api.get (
        "students"
    );

    return request.data;
}

export async function GetStudentByRA(ra) {
    const request = await api.get(
        "students/" + ra
    );

    return request.data;
}

export async function CreateStudent(student){ 
    const request = await api.post(
        "/students",
        student
    )

    return request.data;
}

export async function EditStudent(ra, updatedStudent) {
    const request = await api.put(
        "/students/" + ra,
        updatedStudent
    )

    return request.data;
}

export async function DeleteStudent(ra) {
    const request = await api.delete(
        "/students/" + ra,
    )

    return request.data;
}
