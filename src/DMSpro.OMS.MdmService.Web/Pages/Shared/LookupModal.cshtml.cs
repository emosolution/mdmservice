namespace DMSpro.OMS.MdmService.Web.Pages.Shared
{
    public class LookupModal : MdmServicePageModel
    {
        public string CurrentId { get; set; }
        public string CurrentDisplayName { get; set; }

        public void OnGetAsync(string currentId, string currentDisplayName)
        {
            CurrentId = currentId;
            CurrentDisplayName = currentDisplayName;
        }
    }
}