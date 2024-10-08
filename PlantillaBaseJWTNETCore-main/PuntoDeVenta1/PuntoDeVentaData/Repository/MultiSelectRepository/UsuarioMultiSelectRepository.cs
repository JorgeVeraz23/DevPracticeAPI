﻿using Data.Dto.MultiSelectDTO;
using Data.Entities.MultiSelect;
using Data.Interfaces.MultiSelectInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.MultiSelectRepository
{
    public class UsuarioMultiSelectRepository : UsuarioMultiSelectInterface
    {
        private readonly ApplicationDbContext _context;

        public UsuarioMultiSelectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearUsuarioMultiSelect(UsuarioMultiSelectDTO usuarioDto)
        {
            try
            {
                var usuarioValidacion = await _context.UsuarioMultiSelects.Where(x => x.Nombre.ToUpper().Equals(usuarioDto.Nombre)).FirstOrDefaultAsync();

                if (usuarioValidacion != null)
                {
                    return false;
                }

                UsuarioMultiSelect usuarioMultiSelect = new UsuarioMultiSelect
                {
                    Nombre = usuarioDto.Nombre,
                    IpRegister = "xd",
                    DateRegister = DateTime.Now,
                    UserRegister = "Jorge"
                };


                await _context.UsuarioMultiSelects.AddAsync(usuarioMultiSelect);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        
        }

        public async Task<UsuarioMultiSelectDTO> GetUsuarioByIdAsync(long idUsuarioMultiSelect)
        {
            var usuario = await _context.UsuarioMultiSelects.Where(x => x.IdUsuarioTag == idUsuarioMultiSelect).Select(c => new UsuarioMultiSelectDTO
            {
                IdUsuarioTag = c.IdUsuarioTag,
                Nombre = c.Nombre,
            }
            ).FirstOrDefaultAsync() ?? new UsuarioMultiSelectDTO { };

            return usuario;
        }

        public async Task SaveSelectedTagsAsync(long idUsuarioMultiSelect, List<long> selectedTags)
        {
            //var usuario = await _context.UsuarioMultiSelects.Where(x => x.IdUsuarioTag == idUsuarioMultiSelect).FirstOrDefaultAsync();

            var usuario = await _context.UsuarioMultiSelects.Include(x => x.UsuarioTags).Where(x => x.IdUsuarioTag == idUsuarioMultiSelect)
                .FirstOrDefaultAsync();

            if (usuario.UsuarioTags == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            //Limpiar las relaciones existentes
            usuario.UsuarioTags.Clear();

            //Agregar nuevas relaciones
            foreach (var tagId in selectedTags)
            {
                usuario.UsuarioTags.Add(new UsuarioTag { IdUsuarioMultiSelect = idUsuarioMultiSelect, IdTagMultiSelect = tagId });
            }

            await _context.SaveChangesAsync();  

        }
    }
}
