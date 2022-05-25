import { RestApiService } from '../services/RestAPIService';

export const useRestAPI = () => {
    const restAPIService = new RestApiService();
    const getToken = async (login: string, password: string): Promise<string> => {
        const response = await restAPIService.getToken(login, password);
        console.log(response);
        return response;
    }

    return { getToken };
};