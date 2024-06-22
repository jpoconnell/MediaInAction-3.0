using System;

namespace MediaInAction.EmbyService.EmbyRequestsNs.Dtos
{
    [Serializable]
    public class EmbyRequestCompleteInputDto
    {
        public string Token { get; set; }
        public int TraktTypeId { get; set; }
    }
}