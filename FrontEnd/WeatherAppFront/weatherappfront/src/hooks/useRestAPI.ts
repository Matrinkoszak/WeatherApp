import { RestApiService } from '../services/RestAPIService';

export const useRestAPI = () => {
    const restAPIService = new RestApiService();

    const getToken = async (login: string, password: string): Promise<string> => {
        const response = await restAPIService.getToken(login, password);
        //console.log(response);
        return response;
    }

    const getForecastDays = async (token: string, location: string, startDate: Date, endDate: Date): Promise<object> => {
        const response = await restAPIService.getForecastDays(token, location, startDate, endDate);
        //console.log(response);
        return response;
    }

    return {
        getToken,
        getForecastDays
    };
};