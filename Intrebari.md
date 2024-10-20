#INTREBARI

1. Nu am observat nimic la modificarea valoarea constantei „MatrixMode.Projection.
3.
⚫ Ce este un viewport?
->Un viewport e o portiune a ferestrei in care sunt desenate obiectele. 

⚫ Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
->Reprezinta numarul de cadre randate de OpenGL intr-o secunda. Cu cat frames per seconds creste, cu  calitatea animatiilor se imbunatateste.

⚫ Când este rulată metoda OnUpdateFrame()?
->Este rulata inainte de randarea fiecarui cadru.

⚫ Ce este modul imediat de randare?
->Este o metoda de randare, unde fiecare obiect este descris si trimis catre pipeline-ul grafic in timpul fiecarei iteratii a buclei de randare.
⚫ Care este ultima versiune de OpenGL care acceptă modul imediat?
->3.0

⚫ Când este rulată metoda OnRenderFrame()?
->Dupa metoda onUpdateFrame().

⚫ De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
->Pentru a ajusta viewport-ul si perspectiva.

⚫ Ce reprezintă parametrii metodei
CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
->Field of View(FOV) este unghiul campului vizual in planul vertical;
->Aspect Ratio: raportul dintre latimea si inaltimea viewport-ului(16:9, 4:3, 16:10, etc.);
->Near Clip Pane: distanta pana la planul apropiat de decupare, care defineste cat de aproape trebuie sa fie un obiect pentru a fi vizibil.
->Far Clip Plane: distanta pana la planul indepartat de dcupare, care defineste cat de departe poate fi un obiect pentru a fi vizibil.