import { HttpErrorResponse } from "@angular/common/http";

export function extractFirstErrorMessage(error: HttpErrorResponse): string {
  return error.error.errors[Object.keys(error.error.errors)[0]];
}
