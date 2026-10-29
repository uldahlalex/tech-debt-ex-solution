using LinqToDB;
using LinqToDB.Data;

namespace Infra;

public class MyDatabaseConnection(DataOptions<MyDatabaseConnection> options) : DataConnection(options.Options)
{
    public ITable<Recipe> Recipes => this.GetTable<Recipe>();
    public ITable<Chef> Chefs => this.GetTable<Chef>();
}
