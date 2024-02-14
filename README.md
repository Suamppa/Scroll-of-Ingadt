# Scroll of Ingadt
A simple group game project for the Turku University course Introduction to Game Development Tools

## Projektin kopioiminen Unityyn _(teoriassa)_
Tämä repo itsessään ei riitä suoraksi Unity-projektiksi, joten sen sisältö sellaisenaan ei toimi Unityssä suoraan.
1. Asenna Unity [**2022.3.11f1**](https://unity.com/releases/editor/qa/lts-releases)
2. Luo uusi tyhjä 2D Core Unity-projekti ja seuraa [näitä nopeita ohjeita](https://docs.unity3d.com/Manual/Quickstart2DSetup.html)
3. Esivalmistele Git LFS sekä Git ja Unity yhteiskäyttö
   - Seuraa askelia [tässä artikkelissa](https://medium.com/@linojon/git-and-unity-getting-started-ad7c42be8324) otsikkoon _Making your first commit_ asti (sen jälkeen tulee vain Git-ohjeita). **Älä suorita committeja vielä.**
4. [Näiden ohjeiden mukaisesti](https://www.anchorpoint.app/blog/github-and-unity#:~:text=files%20from%20GitHub.-,Conflicts%20that%20canbe%20resolved%20automatically%20by%20Unity%E2%80%99s%20Smart%20Merge%20Tool,-When%20you%20both) (osio _Conflicts that canbe resolved automatically by Unity’s Smart Merge Tool_):
   - Avaa projektikansiossa .git (piilotetut kansiot pitää asettaa näkyviksi ylhäältä "Näytä -> Piilotetut kohteet"), avaa tiedosto `config` tekstieditorilla ja lisää loppuun seuraava pätkä:
     ```
     [merge]
        tool = unityyamlmerge
     [mergetool "unityyamlmerge"]
        trustExitCode = false
        keepTemporaries = true
        keepBackup = false
        path = 'C:/Program Files/Unity/Hub/Editor/2022.3.11f1/Editor/Data/Tools/UnityYAMLMerge.exe'
        cmd = 'C:/Program Files/Unity/Hub/Editor/2022.3.11f1/Editor/Data/Tools/UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
     ```
     **Jos kansiota .git ei löydy edes piilotettujen kansioiden näyttämisen jälkeen, aja Git Bashillä projektikansiossa `git init`.**
   - **Korvaa tarvittaessa polut `path = ...` ja `cmd = ...` omilla poluilla käyttäen '/' tai '\\\\' (ei '\\')**. Unityn oletuksilla polku on tuo `C:/Program Files/Unity/Hub/Editor/2022.3.11f1/Editor/Data/Tools/UnityYAMLMerge.exe` (voit kokeilla, löytyykö tuo itseltä). Muutoin oma polku löytyy avaamalla Unity Hub -> Installs, näkyvästä listasta 2022.3.11f1 vierestä asetuskuvake ja "Show in explorer", mistä navigoiden vielä `Data/Tools`, josta löytyy `UnityYAMLMerge.exe`.
   - **Jos käytät GitHub Desktop:a**, niin varmista, että repositoryn asetuksissa käytössä on *paikallinen* git config, ei globaali.
5. Tuo GitHub-repon tiedostot omaan projektiisi
   - Yksinkertaisin tapa on ladata repon zip-tiedosto suoraan GitHubista vihreästä _Code_-napista ja korvata oman paikallisen projektin sisältö zipin sisällöllä ylikirjoittaen.
   - Vaihtoehtoisesti voi Gitillä kloonata repon omalle koneelle ensin tyhjään kansioon ja siirtää sieltä paikallisen Unity-projektin päälle, sallien korvaamisen.
   - Kolmas vaihtoehto on määrittää tämä GitHub `origin remote`:ksi paikallisen Unity-projektinsa repoon ja sen kautta [hakea ja tuoda muutokset](https://www.atlassian.com/git/tutorials/syncing).
6. Muutoksia voi sitten helposti seurata ja hakea mm. useimpien IDE:n tai [GitHub Desktopin](https://desktop.github.com/) kautta. **Varmista aina, että muokkaat oikeaa sceneä, joka todennäköisesti on koko projektin ajan sama [SampleScene](Assets/Scenes/SampleScene.unity).**

**Brancheja vaihtaessa voi Unityssä joutua avaamaan käyttämänsä scenen uudelleen.** Jos jokin näyttää oudolta/rikkinäiseltä, kokeile `Assets/Scene` kautta avata scene uudestaan.

**Jos käytät muita IDE:tä kuin Visual Studio Code, käythän lisäämässä siihen generoidut rivit [.gitignore](/.gitignore)-tiedostoon [täältä](https://www.toptal.com/developers/gitignore).**

## Projektissa käytetyt Unity-paketit
Siltä varalta, että paketteja joutuu itse asentamaan ennen projektin tuontia:
- 2D (package): _com.unity.feature.2d_
- Cinemachine: _com.unity.cinemachine_
- Input System: _com.unity.inputsystem_
- JetBrains Rider Editor: _com.unity.ide.rider_
- Recorder: _com.unity.recorder_
- Test Framework: _com.unity.test-framework_
- TextMeshPro: _com.unity.textmeshpro_
- Timeline: _com.unity.timeline_
- Unity UI: _com.unity.ugui_
- Universal RP: _com.unity.render-pipelines.universal_
- Version Control: _com.unity.collab-proxy_
- Visual Scripting: _com.unity.visualscripting_
- Visual Studio Editor: _com.unity.ide.visualstudio_

## GitHub-reposta
Käytössä on sääntö, joka ehkäisee suoraan masteriin pushaamista. Tämä siksi, ettei muiden masteriin tekemä työ katoa Unityn erikoisten mergesekoilujen takia, jos yhdistettävä branch on masteria joltain osin jäljessä (valmisteluissa tehtävä Unity Smart Mergen käyttöönotto toivottavasti ehkäisee tätä). Tee siis muutoksesi aina omalle teemoitetulle branchille. Valmiista muutoksista (branchista) voi sitten luoda pull requestin, jossa pyytää mergeä masteriin. Tämä pull request vaatii yhden hyväksynnän joltain muulta kuin requestin luojalta. Tarkistajan tulee yrittää tulkita muutoksista, ettei mitään muutoksiin liittymätöntä poisteta.

## Kiinnostuneille Unityn Smart Mergen käyttö Gitissä
Unityn Smart Mergen hyödyntäminen aiempien projektin kopiointiohjeiden vaiheen 4 jälkeen on yksinkertaista.
- Aloitetaan merge normaalisti menemällä kohdebranchille, johon aiotaan toinen yhdistää (yleensä `git checkout master`).
- Konfliktien löytyessä Git toimii niin, että se jättää tiedostot ns. konfliktinratkaisumuotoon, jossa tiedostojen molemmat versiot näkyvät otsikoituina tiedoston sisällä, jolloin merge jää tietynlaiseen välitilaan, josta voidaan haluttaessa peruuttaa pois lähtötilaan komennolla `git merge --abort`.
- Konfliktien löydyttyä annetaan komento `git mergetool`. Tämä kutsuu config:n määritelmässä `[merge]` määritettyä työkalua `[mergetool "[NIMI]"]`, tässä tapauksessa `unityyamlmerge` eli tuttavallisemmin Unity Smart Merge.
- Työkalu tekee taikansa ja ilmoittaa onnistumisesta tai mahd. ongelmista.
