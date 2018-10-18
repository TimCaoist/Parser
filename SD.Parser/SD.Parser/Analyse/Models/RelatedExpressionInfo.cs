namespace SD.Parser.Analyse.Models
{
    public class RelatedExpressionInfo : ExpressionInfo
    {
        private const string objstr = "object";

        public string InvokeName { get; set; }

        public string returnTypeName = objstr;

        public string ReturnTypeName {
            get {
                return returnTypeName;
            }
            set {
                returnTypeName = value;
            }
        }
    }
}
