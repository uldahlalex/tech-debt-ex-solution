import { Api } from "../Api.ts";
import { API_URL } from "./config.ts";

export const api = new Api({ baseUrl: API_URL });

/** The generated client rejects with the HTTP response; the server always sends a ProblemDetails body. */
export function getErrorMessage(error: unknown): string {
  if (typeof error === "object" && error !== null && "error" in error) {
    const body: unknown = error.error;
    if (typeof body === "object" && body !== null && "title" in body && typeof body.title === "string") {
      return body.title;
    }
  }
  return "Something went wrong";
}
