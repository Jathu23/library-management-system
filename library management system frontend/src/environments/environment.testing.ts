// src/environments/environment.testing.ts
export const environment = {
    production: false,
    apiBaseUrl: 'https://localhost:7261/api', 
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
    }
    
  };
  