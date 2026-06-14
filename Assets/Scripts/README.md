# Scripts Unity - Sistemas Centrais 2D

Pacote criado conforme as especificacoes dos capitulos de controles, mecanicas principais, fluxograma e especificacao tecnica.

## Entregaveis inclusos

- Jogador com movimento responsivo, aceleracao, desaceleracao e controle aereo de 80%.
- Sistema de pulo com altura aproximada de 2,5 blocos e subida em 0,35 s.
- Coyote Time de 0,1 s.
- Jump Buffer de 0,1 s.
- Estados do jogador: `Idle`, `Running`, `Jumping`, `Falling`, `Dead`.
- Sistema de morte e respawn em 1 segundo.
- Checkpoints persistidos.
- Progressao Fase 1 -> Fase 2 -> Fase 3 -> Fase 4.
- Salvamento com `PlayerPrefs`.
- Menu principal com Novo Jogo e Continuar.
- Glitch de Materializacao com Q.
- Glitch Temporal com E, duracao de 3 s e cooldown de 5 s.
- Coletaveis persistentes.
- Pausa com ESC.
- Camera seguindo o jogador.

## Controles

| Tecla | Funcao |
| --- | --- |
| A / Seta esquerda | Mover esquerda |
| D / Seta direita | Mover direita |
| Espaco | Pular |
| Q | Glitch de Materializacao |
| E | Glitch Temporal |
| ESC | Pausa |

## Configuracao do jogador

1. Copie todos os `.cs` para `Assets/Scripts`.
2. No jogador, adicione `Rigidbody2D`, `Collider2D`, `PlayerController2D` e `PlayerRespawn`.
3. Crie um filho chamado `GroundCheck` nos pes do personagem e arraste para o campo `Ground Check`.
4. Crie a layer `Ground` e marque chao/plataformas com ela.
5. Em `Ground Layer`, selecione `Ground`.

## Checkpoints e morte

- Checkpoint: objeto com `Collider2D` marcado como `Is Trigger` e script `Checkpoint`.
- Cada checkpoint precisa de um `Checkpoint Id` unico, como `fase_1_cp_01`.
- Zona de morte: objeto com `Collider2D` marcado como `Is Trigger` e script `DeathZone`.

## Progressao

1. Crie um objeto vazio chamado `LevelManager`.
2. Adicione `LevelProgressionManager`.
3. Configure `Current Level` e `Next Scene Name`.
4. No fim da fase, crie um trigger com `LevelExit` apontando para o `LevelManager`.

## Menu principal

- Em um objeto da cena de menu, adicione `MainMenuManager`.
- Configure `Level Scene Names` com as cenas `Fase1`, `Fase2`, `Fase3`, `Fase4`.
- Ligue os botoes da UI aos metodos `NewGame`, `ContinueGame` e `QuitGame`.

## Materializacao

1. Em cada plataforma azul/vermelha, adicione `MaterializationPlatform`.
2. Defina o grupo como `Blue` ou `Red`.
3. Em um objeto da cena, adicione `MaterializationGlitch`.
4. Arraste todas as plataformas para o array `Platforms`.

Regra implementada: azul inicia ativo, vermelho inicia invisivel e sem colisao. Ao pressionar Q, alterna instantaneamente.

## Glitch Temporal

1. Nos obstaculos afetados, adicione `TemporalFreezableRigidbody2D`.
2. Em um objeto da cena, adicione `TemporalGlitch`.
3. Arraste os objetos afetados para `Affected Objects`.

Regra implementada: E congela por 3 segundos e so pode ser usado de novo apos 5 segundos.

## Fragmentos de memoria

- Crie objetos com `Collider2D` marcado como `Is Trigger`.
- Adicione `MemoryFragment`.
- Cada fragmento precisa de um `Fragment Id` unico.
- Depois de coletado, o fragmento fica salvo e nao reaparece.
- `SaveManager.IsExtendedEndingUnlocked` retorna verdadeiro quando 30 fragmentos foram coletados.

Distribuicao sugerida:

| Fase | Fragmentos |
| --- | ---: |
| Fase 1 | 2 |
| Fase 2 | 4 |
| Fase 3 | 8 |
| Fase 4 | 16 |

## Observacoes tecnicas

- O pacote usa `PlayerPrefs` para simplificar o salvamento.
- A colisao principal e feita pela fisica 2D da Unity.
- Nao ha pulo duplo, dash ou wall jump.
- Glitches podem ser usados no ar e nao interrompem o movimento.
