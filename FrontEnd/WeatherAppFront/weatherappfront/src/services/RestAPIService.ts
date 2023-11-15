import axios from 'axios';

export class RestApiService {
    public getToken = (login: string, password: string) => {
        const url = "https://localhost:44372/api/authToken/" + login + "/" + password;
        return axios({
            method: 'get',
            url: url
        }).then((response) => {
            //console.log(response);
            return response.data.code;
        })
    }

    public getForecastDays = (token: string, location: string, startDate: Date, endDate: Date) => {
        const url = "https://localhost:44372/api/forcastdays/" + token + "/" + location + "/" + startDate.toDateString() + "/" + endDate.toDateString();
        console.log(url);
        return axios({
            method: 'get',
            url: url
        }).then((response) => {
            console.log(response);
            return response.data;
        })
    }
}