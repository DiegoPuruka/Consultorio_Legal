namespace CL.Core.Shared.ModelViews
{
    public class NovoEndereco
    {
        ///<example>9790530</example>
        public int CEP { get; set; }

        ///<example>SP</example>
        public string Estado { get; set; }

        ///<example>São Bernardo do Campo</example>
        public string Cidade { get; set; }

        ///<example>Avenida Jesus de Nazareth</example>
        public string Logradouro { get; set; }

        ///<example>758</example>
        public string Numero { get; set; }

        ///<example>Casa 5</example>
        public string Complemento { get; set; }
    }
}