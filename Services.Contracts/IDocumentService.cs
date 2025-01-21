using LMS.Shared.DTOs.DocumentDTOs;
using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDocumentService
    {
        Task<(IEnumerable<DocumentDto> Documents, int TotalCount)> GetDocumentsAsync(int courseId,
               int moduleId,
               int activityId,
               int? pageNr = null,
               int? pageSize = null,
               string? sortBy = null,
               bool isAscending = true,
               string? filteringValue = null);
        Task<DocumentDto> GetDocumentByIdAsync(int id);
        Task<bool> UpdateDocumentAsync(int id, int courseId, int moduleId, int activityId, DocumentUpdateDTO moduleDto);
        Task<DocumentDto> CreateDocumentAsync(DocumentCreateDTO moduleDto, int courseId, int moduleId, int activityId);
        Task<bool> DeleteDocumentAsync(int id);
    }
}
