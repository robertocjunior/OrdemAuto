import config from './config';

const { baseApiUrl } = config;

export default {

    ordem: {
        baseURL: `${baseApiUrl}/OrdemServico`,
        endpoints: {
            pesquisar: "/",
            consultar: "/Consultar",
            cadastrar: "/Cadastrar",
            editar: "/"
        }
    },
        
    cadastro: {
        baseURL: `${baseApiUrl}/Cadastro`,
        pecas: {
            baseURL: `${baseApiUrl}/Cadastro`,
            endpoints: {
                consultar: "/pecas",             
                adicionar: "/pecas/adicionar",   
                editar: "/pecas/editar"         
            }
        },
    },
    veiculo: {
        baseURL: `${baseApiUrl}/Cadastro`,
        endpoints: {
            pesquisar: "/veiculos/PesquisarVeiculos",           // GET /Cadastro/veiculos/{id}
            consultar: "/veiculos",           // GET /Cadastro/veiculos/{id}
            adicionar: "/veiculos/adicionar", // POST
            editar: "/veiculos/editar"        // PUT
        }
    }

};
