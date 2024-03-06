﻿using AntDesign;
using Microsoft.AspNetCore.Components;
using AntSK.Domain.Repositories;
using AntSK.Models;
using System.IO;

namespace AntSK.Pages.KmsPage
{

    public partial class AddKms
    {
        [Inject]
        protected IKmss_Repositories _kmss_Repositories { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected MessageService? Message { get; set; }

        private readonly Kmss _kmsModel = new Kmss() ;

        private void HandleSubmit()
        {
            _kmsModel.Id = Guid.NewGuid().ToString();
            if (_kmss_Repositories.IsAny(p => p.Name == _kmsModel.Name))
            {
                _ = Message.Error("名称已存在！", 2);
                return;
            }
            if (_kmsModel.OverlappingTokens >= _kmsModel.MaxTokensPerLine || _kmsModel.OverlappingTokens >= _kmsModel.MaxTokensPerParagraph)
            {
                _ = Message.Error("重叠部分需小于行切片和段落切片！", 2);
                return;
            }

            if (_kmsModel.MaxTokensPerLine >= _kmsModel.MaxTokensPerParagraph)
            {
                _ = Message.Error("行切片需小于段落切片！", 2);
                return;
            }
            _kmss_Repositories.Insert(_kmsModel);

            NavigationManager.NavigateTo("/kmslist");

        }
    }    
}
