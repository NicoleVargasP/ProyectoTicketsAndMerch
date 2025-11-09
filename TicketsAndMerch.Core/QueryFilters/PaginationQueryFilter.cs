using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.QueryFilters
{
    public abstract class PaginationQueryFilter
    {
        /// <summary>
        /// Cantidad de registros por pagina
        /// </summary>
        [SwaggerSchema("Cantidad de registros por pagina")]
        public int PageSize { get; set; }


        /// <summary>
        /// Numero de pagina a mostrar
        /// </summary>
        [SwaggerSchema("Numero de pagina a mostrar")]
        public int PageNumber { get; set; }
    }
}
