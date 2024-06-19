using BITS.Data;
using BITS.Models;
using Microsoft.AspNetCore.Mvc;
namespace BITS.Services;

public class SourceServices
{
    public static async Task<string> GetSourceName(int? id, BITSContext _context)
    {
        if (id == null)
            return "Id not exist.";

        var source = await _context.Source.FindAsync(id);
        if(source == null)
            return "Source not found";

        return source.Name;
    }
}
