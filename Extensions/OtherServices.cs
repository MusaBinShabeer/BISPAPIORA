namespace BISPAPIORA.Extensions
{
    public class OtherServices
    {
        public bool Check(object model)
        {
            try
            {
                if (model.GetType() == typeof(byte[]))
                {
                    byte[] newLogo = Convert.FromBase64String(model.ToString());
                    if (newLogo.Length > 0 && newLogo != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(model.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
