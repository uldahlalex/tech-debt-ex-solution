import { useState, type FormEvent } from "react";

type Props = {
  onCreate: (chefName: string) => Promise<boolean>;
};

export function CreateChefForm({ onCreate }: Props) {
  const [chefName, setChefName] = useState("");

  async function submit(e: FormEvent) {
    e.preventDefault();
    if (await onCreate(chefName)) setChefName("");
  }

  return (
    <form aria-label="Create chef" onSubmit={submit}>
      <label>
        Chef name
        <input value={chefName} onChange={e => setChefName(e.target.value)} />
      </label>
      <button type="submit">Create chef</button>
    </form>
  );
}
