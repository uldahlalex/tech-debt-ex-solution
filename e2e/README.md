# End-to-end tests (the safety net)

These tests start the **real** API and the **real** frontend, then use them the way a user
(or another program) would: over HTTP and in a browser. They don't look at the code at all,
which is why they keep working while you move code around.

## Running

You need the .NET 10 SDK, [Bun](https://bun.sh) and Node.js 20 or newer.

```bash
cd client && bun install && cd ..   # once
cd e2e
bun install                         # once
bun run install-browser             # once: downloads Chromium for Playwright
bun run test                        # run everything (about 10 seconds after the first build)
bun run test:api                    # only the API tests
bun run test:ui                     # Playwright's interactive UI, great for seeing what failed
```

The tests start the servers themselves on ports **5150** (API) and **3150** (frontend),
each time with a fresh, empty database. Stop your own `dotnet run` first, or the API
port will already be taken.

When a test fails, open the HTML report (`bunx playwright show-report`) to see a trace
with screenshots of every step.

## The contract: what the tests rely on

Refactor anything you like, as long as these stay the same:

| Thing | Must stay |
|---|---|
| Endpoints | `GET /GetRecipes?page=&resultsPerPage=`, `POST /CreateRecipe`, `GET /GetChefs`, `POST /CreateChef` |
| JSON | field names `recipeId`, `recipeName`, `cookingMinutes`, `course`, `chefId`, `chef`, `chefName`; courses are the strings `Starter`, `MainCourse`, `Dessert`, `Drink` |
| Errors | invalid input gives HTTP **400** with a JSON body containing a readable `title` |
| Rules | recipe name 1–100 characters (trimmed), 1–1440 cooking minutes, chef must exist, chef name 1–60 characters, 1–50 results per page, recipes and chefs sorted by name |
| Seed data | an empty database gets chef `1` "alice" and recipe `1` "pancakes" (20 min, Dessert) |
| Config | the API reads its connection string from `ConnectionStrings:Cookbook` and listens on the URL given with `--urls` |
| UI | the heading "Cookbook", the list labelled "Recipes", the forms "Create recipe"/"Create chef", the field labels, button texts, "Page N", 5 recipes per page and the line format `<name> by <chef> · <minutes> min · <course label>` |

**Don't edit the existing tests.** If you find a bug, you may *add* a new test that shows it.
