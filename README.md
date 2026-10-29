# BlazorWasmBrasilApiCep

Aplicação Blazor WebAssembly da atividade `12.net_blazor_vscode.pdf`: consulta CEP na BrasilAPI e apresenta o endereço e os dados do IBGE quando retornados.

## Executar

Pré-requisitos: SDK .NET 10, navegador com WebAssembly e conexão à Internet.

```powershell
dotnet restore
dotnet build -c Release
dotnet run --urls http://localhost:5112
```

Abra http://localhost:5112. Informe um CEP com oito dígitos, com ou sem hífen, e clique em **Consultar CEP** ou pressione Enter. Experimente `30130-000`: Avenida Afonso Pena, Centro, Belo Horizonte/MG.

## Contrato da API

A consulta usa `GET https://brasilapi.com.br/api/cep/v1/{cep}`. O `/api/` do endpoint foi mantido conforme a [documentação oficial](https://brasilapi.com.br/docs#tag/CEP), embora tenha sido omitido em um dos slides.

| Informação na tela | Campo JSON |
|---|---|
| CEP | `cep` |
| Estado | `state` |
| Cidade | `city` |
| Bairro | `neighborhood` |
| Logradouro | `street` |
| Serviço | `service` |
| Código IBGE da cidade | `ibge.city` |
| Código IBGE do estado | `ibge.state` |

A disponibilidade dos campos depende do serviço consultado pela BrasilAPI. Campos ausentes são informados como indisponíveis, sem criar valores. O CEP `30130000` retornou `ibge.city = 3106200` e `ibge.state = 31` na validação.

## Comportamento

- Validação local: apenas oito dígitos ASCII, opcionalmente separados por hífen após o quinto dígito.
- Busca assíncrona com indicação de carregamento e bloqueio de envio duplicado.
- Mensagens distintas para CEP inexistente, rede indisponível, timeout e resposta inválida.
- Dados antigos são removidos ao começar uma nova consulta.
- Interface em português, rótulos acessíveis e layout adaptado a telas estreitas.

## Estrutura

`Pages/Home.razor` contém a consulta e apresentação, `Models/CepResponse.cs` mapeia a resposta e `Program.cs` configura o HttpClient com timeout de vinte segundos.

## Verificação do contrato

```powershell
dotnet run --project tests/CepChecks.csproj
```

A verificação consulta a API real, confere endereço e IBGE do CEP de exemplo, resposta 404 para CEP inexistente e desserialização de campos opcionais ausentes. Exige acesso à Internet.

## Entrega

Repositório solicitado: `blazor-brasilapi-cep`. O `.gitignore` do Visual Studio exclui `bin/`, `obj/` e configurações locais. Para limpar a saída da compilação, execute `dotnet clean -c Release`.

Código sob licença MIT.
