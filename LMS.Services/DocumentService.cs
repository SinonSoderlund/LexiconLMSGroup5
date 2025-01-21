using AutoMapper;
using Domain.Contracts;
using LMS.Shared.DTOs.DocumentDTOs;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using System;
using System.Collections.Generic;
using Domain.Models.Entities;

namespace LMS.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
      //private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public DocumentService(IUnitOfWork uow, 
            IMapper mapper, 
           // IWebHostEnvironment environment, 
            UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _mapper = mapper;
          //  _environment = environment;
            _userManager = userManager;
        }

        public Task<DocumentDto> CreateDocumentAsync(DocumentCreateDTO moduleDto, int courseId, int moduleId, int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDocumentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentDto> GetDocumentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<DocumentDto> Documents, int TotalCount)> GetDocumentsAsync(int courseId, int moduleId, int activityId, int? pageNr = null, int? pageSize = null, string? sortBy = null, bool isAscending = true, string? filteringValue = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentAsync(int id, int courseId, int moduleId, int activityId, DocumentUpdateDTO moduleDto)
        {
            throw new NotImplementedException();
        }
    }
}
