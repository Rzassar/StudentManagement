export interface Student {
    id: number;
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    email: string;
}

export interface CreateStudent {
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    email: string;
}