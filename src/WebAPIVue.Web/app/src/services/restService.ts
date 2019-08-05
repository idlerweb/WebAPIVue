import { BaseModel } from '../models/base';

export class RestService<T extends BaseModel> {

    private readonly _repository: string;

    constructor(repository: string) {
        this._repository = repository;
    }

    public getList(): Promise<T[]> {
        const requestOptions = {
            method: 'GET',
        };

        const url = config.apiUrl + this._repository;

        return fetch(url, requestOptions).then((r) => handleResponse<T[]>(r), handleError);
    }
    public get(id: string | number): Promise<T> {
        const requestOptions = {
            method: 'GET',
        };

        let url = config.apiUrl + this._repository;

        if (id) {
            url = url + '/' + id;
        }

        return fetch(url, requestOptions).then((r) => handleResponse<T>(r), handleError);
    }
    public post(data: T) {

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        };

        const url = config.apiUrl + this._repository;

        return fetch(url, requestOptions).then((r) => handleResponse<T>(r), handleError);
    }
    public put(data: T): Promise<T> {

        const requestOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        };

        const url = config.apiUrl + this._repository;

        return fetch(url, requestOptions).then((r) => handleResponse<T>(r), handleError);
    }
    public delete(id: number): Promise<T> {

        const requestOptions = {
            method: 'DELETE',
        };

        const url = config.apiUrl + this._repository;

        return fetch(url + '/' + id, requestOptions).then((r)  => handleResponse<T> (r), handleError);
    }
}

const config = {
    apiUrl: '/',
};

function handleResponse<T>(response: any): Promise<T> {
    return new Promise<T>((resolve, reject) => {
        if (response.ok) {
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                response.json().then((json: any) => resolve(json));
            } else {
                response.text().then((text: any) => resolve(text));
            }
        } else {
            response.json().then((json: any) => reject(json));
        }
    });
}

function handleError(error: any) {
    return Promise.reject(error && error.message);
}
