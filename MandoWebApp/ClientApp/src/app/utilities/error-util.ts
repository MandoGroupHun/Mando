import { HttpErrorResponse } from "@angular/common/http";

export function extractFirstErrorMessage(error: HttpErrorResponse): string {
  if (error === undefined || error.error === undefined) {
    return '-';
  }

  if (typeof error.error === 'string') {
    return error.error as string;
  }

  if (error.error.errors === undefined || Object.keys(error.error.errors).length === 0) {
    return '-';
  }

  return error.error.errors[Object.keys(error.error.errors)[0]];
}
