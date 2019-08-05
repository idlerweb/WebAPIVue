import { BaseModel } from './base';

export class User extends BaseModel {
    public userName: string | undefined;
    public departmentId: number | undefined;
    public departmentName: string | undefined;
}
