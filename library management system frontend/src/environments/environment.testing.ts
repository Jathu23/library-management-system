import { HttpClient } from "@angular/common/http";
import { jwtDecode } from "jwt-decode";

export const environment = {
    production: false,
    apiBaseUrl: 'https://localhost:7261/api', 
    resourcBaseUrl: 'https://localhost:7261/',
    
    decodeTokenManually(token: string | null): any {
        if (!token) {
            console.error('Token is null or empty');
            return null;
        }
        try {
            const payload = token.split('.')[1];
            const decodedPayload = JSON.parse(atob(payload));
            return decodedPayload;
        } catch (error) {
            console.error('Error decoding token', error);
            return null;
        }
    },

    getTokenData(): any {
        const token = localStorage.getItem('token');
        if (!token) {
            console.warn('No token found in localStorage');
            return null; 
        }
        try {
            const decodedData = jwtDecode<any>(token); 
            console.log("user",decodedData);
            return decodedData;
        } catch (error) {
            console.error('Error decoding the token:', error);
            return null; 
        }
    },

    async fetchUserDataById(http: HttpClient, id: number): Promise<any> {
        const url = `https://localhost:7261/api/User/getUserByIdid?id=${id}`;
        try {
            const response = await http.get(url).toPromise();
            console.log('Fetched User Data:', response);
            return response;
        } catch (error) {
            console.error('Error fetching user data:', error);
            return null;
        }
    }
};
