import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = new Router();

  const token = localStorage.getItem('token');
  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const userRole = payload.role;
    const allowedRoles = route.data['roles'] as Array<string>;

    if (allowedRoles.includes(userRole)) {
      return true;
    } else {
      router.navigate(['/']);
      return false;
    }
  } catch (error) {
    console.error('Invalid token:', error);
    router.navigate(['/login']);
    return false;
  }
};
