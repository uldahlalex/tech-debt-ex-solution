import { useState, type FormEvent } from "react";
import { Course, type ChefDto, type CreateRecipeRequestDto } from "../../Api.ts";
import { allCourses, courseLabels } from "../courses.ts";

type Props = {
  chefs: ChefDto[];
  onCreate: (request: CreateRecipeRequestDto) => Promise<boolean>;
};

export function CreateRecipeForm({ chefs, onCreate }: Props) {
  const [recipeName, setRecipeName] = useState("");
  const [cookingMinutes, setCookingMinutes] = useState(30);
  const [chefId, setChefId] = useState("");
  const [course, setCourse] = useState<Course>(Course.MainCourse);

  const selectedChefId = chefId || chefs[0]?.chefId || "";

  async function submit(e: FormEvent) {
    e.preventDefault();
    const created = await onCreate({ recipeName, cookingMinutes, chefId: selectedChefId, course });
    if (created) setRecipeName("");
  }

  return (
    <form aria-label="Create recipe" onSubmit={submit}>
      <label>
        Name
        <input value={recipeName} onChange={e => setRecipeName(e.target.value)} />
      </label>
      <label>
        Minutes
        <input type="number" value={cookingMinutes} onChange={e => setCookingMinutes(e.target.valueAsNumber)} />
      </label>
      <label>
        Chef
        <select value={selectedChefId} onChange={e => setChefId(e.target.value)}>
          {chefs.map(c => (
            <option key={c.chefId} value={c.chefId}>{c.chefName}</option>
          ))}
        </select>
      </label>
      <label>
        Course
        <select value={course} onChange={e => setCourse(e.target.value as Course)}>
          {allCourses.map(c => (
            <option key={c} value={c}>{courseLabels[c]}</option>
          ))}
        </select>
      </label>
      <button type="submit">Create recipe</button>
    </form>
  );
}
