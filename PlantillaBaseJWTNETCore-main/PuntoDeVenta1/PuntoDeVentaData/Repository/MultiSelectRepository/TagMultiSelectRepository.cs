using Data.Dto.MultiSelectDTO;
using Data.Entities.MultiSelect;
using Data.Interfaces.MultiSelectInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.MultiSelectRepository
{
    public class TagMultiSelectRepository : TagMultiSelectInterface
    {
        private readonly ApplicationDbContext _context;

        public TagMultiSelectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CrearTagMuliSelect(TagMultiSelectDTO tagMultiSelectDTO)
        {
            try{

                TagMultiSelect tagMultiSelect = new TagMultiSelect
                {
                    Nombre = tagMultiSelectDTO.Nombre,
                    DateRegister = DateTime.Now,
                    UserRegister = "Jorge XD",
                    IpRegister = "ASDADSASD",
                };

                await _context.TagMultiSelects.AddAsync(tagMultiSelect);

                await _context.SaveChangesAsync();

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
