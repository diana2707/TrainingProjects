using Cafe.Application.DTOs;
using Cafe.Domain.Models;

namespace Cafe.Application.Assemblers
{
    public interface IBeverageAssembler
    {
        public IBeverage Assemble(BeverageDetails beverageDetails);
    }
}
