import { Course } from "../Api.ts";

/** Display names for every course. `Record<Course, ...>` makes the compiler complain if a course is missing. */
export const courseLabels: Record<Course, string> = {
  [Course.Starter]: "Starter",
  [Course.MainCourse]: "Main course",
  [Course.Dessert]: "Dessert",
  [Course.Drink]: "Drink",
};

export const allCourses = Object.values(Course);
