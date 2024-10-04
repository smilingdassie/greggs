using Greggs.Products.Api.Models;
using System.Collections.Generic;

namespace Greggs.Products.Api.DataAccess;

public interface IDataAccess<out T>
{
    IEnumerable<T> List(int? pageStart, int? pageSize);
    IEnumerable<T> ListHardCoded(int? pageStart, int? pageSize);
}