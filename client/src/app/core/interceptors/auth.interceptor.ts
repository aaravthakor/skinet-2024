import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const clonnedRequest = req.clone({
    withCredentials:true
  });

  

  return next(clonnedRequest);
};
