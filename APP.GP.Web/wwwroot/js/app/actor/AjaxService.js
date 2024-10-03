class AjaxService {
    static get(url) {
        return $.ajax({ url, type: 'GET' });
    }

    static post(url, data) {
        return $.ajax({ url, type: 'POST', data, contentType: false, processData: false });
    }
}
