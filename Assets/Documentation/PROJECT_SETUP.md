# Configuração do Projeto - Glitch Run

## Versão da Unity

Unity 6000.3.10f1

---

## Controle de Versão

Repositório hospedado no GitHub.

Branch principal:

main

Git LFS habilitado.

---

## Configurações da Unity

Editor Settings:

Version Control Mode:
Visible Meta Files

Asset Serialization:
Force Text

---

## Estrutura de Pastas

Assets/

Art/

* Sprites
* Backgrounds
* UI

Audio/

* Music
* SFX

Scripts/

Prefabs/

Scenes/

Animations/

Materials/

Documentation/

---

## Estrutura de Cenas

MainMenu

Level_01

TestScene

---

## Convenções de Nomenclatura

### Scripts

Utilizar PascalCase.

Exemplos:

PlayerController.cs

PauseManager.cs

CheckpointManager.cs

GlitchMaterialization.cs

---

### Assets Visuais

Utilizar os identificadores definidos no Documento de Assets.

Exemplos:

CHAR_001_NEX

CHAR_002_NULL

BG_001_Inicializacao

PLAT_001_PlataformaComum

OBS_001_Espinhos

OBS_002_Laser

---

### Áudios

Utilizar os identificadores definidos no Documento de Áudio.

Exemplos:

MUS_001_MenuPrincipal

MUS_002_Fase1

SFX_001_Pulo

SFX_006_GlitchMaterializacao

---

### Prefabs

Utilizar o identificador da categoria correspondente.

Exemplos:

PF_CHAR_001_NEX

PF_OBS_001_Espinhos

PF_PLAT_002_PlataformaAzul

PF_PLAT_003_PlataformaVermelha

PF_COL_001_FramentoMemoria

---

### Cenas

Utilizar PascalCase.

Exemplos:

MainMenu

Level_01

Level_02

CreditsScene

TestScene

---

## Fluxo de Trabalho

1. Atualizar o projeto antes de iniciar qualquer atividade.
2. Realizar alterações localmente.
3. Testar antes de realizar commit.
4. Utilizar mensagens de commit descritivas.
5. Enviar as alterações para o GitHub.

---

## Exemplos de Commits

Adicionar movimentação do jogador

Implementar sistema de pausa

Criar prefab da plataforma de glitch

Corrigir colisão da fase 01

---

## Status Atual

### Infraestrutura

* Projeto Unity configurado.
* Repositório GitHub configurado e operacional.
* Git LFS habilitado.
* Estrutura de pastas definida.
* Documentação principal integrada ao projeto.
* Build Profiles configurados.

---

### Sistema de Input

O projeto utiliza o sistema:

Input System Package (New)

Entradas centralizadas através de:

* InputSystem_Actions.inputactions
* InputReader.cs

Não utilizar:

* Input.GetAxisRaw()
* Input.GetButtonDown()
* Input.GetKeyDown()

Novas entradas devem ser implementadas através do Input System.

---

### Sistemas Implementados e Validados

* Movimento do jogador.
* Sistema de salto.
* Coyote Time.
* Jump Buffer.
* Checkpoints.
* Respawn.
* Death Zones.
* Glitch de Materialização.
* Sistema de Pause.
* Camera Follow.

Todos os sistemas acima foram testados e encontram-se funcionais na TestScene.

---

### Prefabs Disponíveis

* PF_Player
* PF_Checkpoint
* PF_DeathZone
* PF_PLAT_001_Blue
* PF_PLAT_002_Red

Os prefabs devem ser reutilizados na construção das fases sempre que possível.

---

### Cena de Testes

A cena TestScene é utilizada para validação técnica e prototipação de sistemas.

Novas mecânicas devem ser testadas primeiro na TestScene antes de serem integradas às fases jogáveis.

---

## Organização da Hierarchy (TestScene)

Estrutura atualmente utilizada:

Main Camera

Managers
* InputReader
* PauseManager
* MaterializationManager

Player
* GroundCheck

Environment
* Ground
* Checkpoint_01
* DeathZone
* BluePlatform
* RedPlatform

Esta organização deve ser mantida como referência para futuras cenas de teste.