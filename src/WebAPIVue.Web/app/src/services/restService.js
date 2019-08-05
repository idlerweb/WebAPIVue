export class RestService {
    constructor(repository) {
        this._repository = repository;
    }
    getList() {
        const requestOptions = {
            method: 'GET',
        };
        const url = config.apiUrl + this._repository;
        return fetch(url, requestOptions).then((r) => handleResponse(r), handleError);
    }
    get(id) {
        const requestOptions = {
            method: 'GET',
        };
        let url = config.apiUrl + this._repository;
        if (id) {
            url = url + '/' + id;
        }
        return fetch(url, requestOptions).then((r) => handleResponse(r), handleError);
    }
    post(data) {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        };
        const url = config.apiUrl + this._repository;
        return fetch(url, requestOptions).then((r) => handleResponse(r), handleError);
    }
    put(data) {
        const requestOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        };
        const url = config.apiUrl + this._repository;
        return fetch(url, requestOptions).then((r) => handleResponse(r), handleError);
    }
    delete(id) {
        const requestOptions = {
            method: 'DELETE',
        };
        const url = config.apiUrl + this._repository;
        return fetch(url + '/' + id, requestOptions).then((r) => handleResponse(r), handleError);
    }
}
const config = {
    apiUrl: '/',
};
function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                response.json().then((json) => resolve(json));
            }
            else {
                response.text().then((text) => resolve(text));
            }
        }
        else {
            response.json().then((json) => reject(json));
        }
    });
}
function handleError(error) {
    return Promise.reject(error && error.message);
}
//# sourceMappingURL=restService.js.map