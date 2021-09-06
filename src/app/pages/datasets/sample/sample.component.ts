import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sample',
  templateUrl: './sample.component.html',
  styleUrls: ['./sample.component.scss']
})
export class SampleComponent {
  data = `
  "LatD", "LatM", "LatS", "NS", "LonD", "LonM", "LonS", "EW", "City", "State"
   41,    5,   59, "N",     80,   39,    0, "W", "Youngstown", OH
   42,   52,   48, "N",     97,   23,   23, "W", "Yankton", SD
   46,   35,   59, "N",    120,   30,   36, "W", "Yakima", WA
   42,   16,   12, "N",     71,   48,    0, "W", "Worcester", MA
   43,   37,   48, "N",     89,   46,   11, "W", "Wisconsin Dells", WI
   36,    5,   59, "N",     80,   15,    0, "W", "Winston-Salem", NC
   49,   52,   48, "N",     97,    9,    0, "W", "Winnipeg", MB
   39,   11,   23, "N",     78,    9,   36, "W", "Winchester", VA
   34,   14,   24, "N",     77,   55,   11, "W", "Wilmington", NC
   39,   45,    0, "N",     75,   33,    0, "W", "Wilmington", DE
   48,    9,    0, "N",    103,   37,   12, "W", "Williston", ND
   41,   15,    0, "N",     77,    0,    0, "W", "Williamsport", PA
   37,   40,   48, "N",     82,   16,   47, "W", "Williamson", WV
   33,   54,    0, "N",     98,   29,   23, "W", "Wichita Falls", TX
   37,   41,   23, "N",     97,   20,   23, "W", "Wichita", KS
   40,    4,   11, "N",     80,   43,   12, "W", "Wheeling", WV
   26,   43,   11, "N",     80,    3,    0, "W", "West Palm Beach", FL
   47,   25,   11, "N",    120,   19,   11, "W", "Wenatchee", WA
   41,   25,   11, "N",    122,   23,   23, "W", "Weed", CA
   31,   13,   11, "N",     82,   20,   59, "W", "Waycross", GA
   44,   57,   35, "N",     89,   38,   23, "W", "Wausau", WI
   42,   21,   36, "N",     87,   49,   48, "W", "Waukegan", IL
   44,   54,    0, "N",     97,    6,   36, "W", "Watertown", SD
   43,   58,   47, "N",     75,   55,   11, "W", "Watertown", NY
   42,   30,    0, "N",     92,   20,   23, "W", "Waterloo", IA
   41,   32,   59, "N",     73,    3,    0, "W", "Waterbury", CT
   38,   53,   23, "N",     77,    1,   47, "W", "Washington", DC
   41,   50,   59, "N",     79,    8,   23, "W", "Warren", PA
   46,    4,   11, "N",    118,   19,   48, "W", "Walla Walla", WA
   31,   32,   59, "N",     97,    8,   23, "W", "Waco", TX
   38,   40,   48, "N",     87,   31,   47, "W", "Vincennes", IN
   28,   48,   35, "N",     97,    0,   36, "W", "Victoria", TX
   32,   20,   59, "N",     90,   52,   47, "W", "Vicksburg", MS
   49,   16,   12, "N",    123,    7,   12, "W", "Vancouver", BC
   46,   55,   11, "N",     98,    0,   36, "W", "Valley City", ND
   30,   49,   47, "N",     83,   16,   47, "W", "Valdosta", GA
   43,    6,   36, "N",     75,   13,   48, "W", "Utica", NY
   39,   54,    0, "N",     79,   43,   48, "W", "Uniontown", PA
   32,   20,   59, "N",     95,   18,    0, "W", "Tyler", TX
   42,   33,   36, "N",    114,   28,   12, "W", "Twin Falls", ID
   33,   12,   35, "N",     87,   34,   11, "W", "Tuscaloosa", AL
   34,   15,   35, "N",     88,   42,   35, "W", "Tupelo", MS
   36,    9,   35, "N",     95,   54,   36, "W", "Tulsa", OK
   32,   13,   12, "N",    110,   58,   12, "W", "Tucson", AZ
   37,   10,   11, "N",    104,   30,   36, "W", "Trinidad", CO
   40,   13,   47, "N",     74,   46,   11, "W", "Trenton", NJ
   44,   45,   35, "N",     85,   37,   47, "W", "Traverse City", MI
   43,   39,    0, "N",     79,   22,   47, "W", "Toronto", ON
   39,    2,   59, "N",     95,   40,   11, "W", "Topeka", KS
   41,   39,    0, "N",     83,   32,   24, "W", "Toledo", OH
   33,   25,   48, "N",     94,    3,    0, "W", "Texarkana", TX
   39,   28,   12, "N",     87,   24,   36, "W", "Terre Haute", IN
   27,   57,    0, "N",     82,   26,   59, "W", "Tampa", FL
   30,   27,    0, "N",     84,   16,   47, "W", "Tallahassee", FL
   47,   14,   24, "N",    122,   25,   48, "W", "Tacoma", WA
   43,    2,   59, "N",     76,    9,    0, "W", "Syracuse", NY
   32,   35,   59, "N",     82,   20,   23, "W", "Swainsboro", GA
   33,   55,   11, "N",     80,   20,   59, "W", "Sumter", SC
   40,   59,   24, "N",     75,   11,   24, "W", "Stroudsburg", PA
   37,   57,   35, "N",    121,   17,   24, "W", "Stockton", CA
   44,   31,   12, "N",     89,   34,   11, "W", "Stevens Point", WI
   40,   21,   36, "N",     80,   37,   12, "W", "Steubenville", OH
   40,   37,   11, "N",    103,   13,   12, "W", "Sterling", CO
   38,    9,    0, "N",     79,    4,   11, "W", "Staunton", VA
   39,   55,   11, "N",     83,   48,   35, "W", "Springfield", OH
   37,   13,   12, "N",     93,   17,   24, "W", "Springfield", MO
   42,    5,   59, "N",     72,   35,   23, "W", "Springfield", MA
   39,   47,   59, "N",     89,   39,    0, "W", "Springfield", IL
   47,   40,   11, "N",    117,   24,   36, "W", "Spokane", WA
   41,   40,   48, "N",     86,   15,    0, "W", "South Bend", IN
   43,   32,   24, "N",     96,   43,   48, "W", "Sioux Falls", SD
   42,   29,   24, "N",     96,   23,   23, "W", "Sioux City", IA
   32,   30,   35, "N",     93,   45,    0, "W", "Shreveport", LA
   33,   38,   23, "N",     96,   36,   36, "W", "Sherman", TX
   44,   47,   59, "N",    106,   57,   35, "W", "Sheridan", WY
   35,   13,   47, "N",     96,   40,   48, "W", "Seminole", OK
   32,   25,   11, "N",     87,    1,   11, "W", "Selma", AL
   38,   42,   35, "N",     93,   13,   48, "W", "Sedalia", MO
   47,   35,   59, "N",    122,   19,   48, "W", "Seattle", WA
   41,   24,   35, "N",     75,   40,   11, "W", "Scranton", PA
   41,   52,   11, "N",    103,   39,   36, "W", "Scottsbluff", NB
   42,   49,   11, "N",     73,   56,   59, "W", "Schenectady", NY
   32,    4,   48, "N",     81,    5,   23, "W", "Savannah", GA
   46,   29,   24, "N",     84,   20,   59, "W", "Sault Sainte Marie", MI
   27,   20,   24, "N",     82,   31,   47, "W", "Sarasota", FL
   38,   26,   23, "N",    122,   43,   12, "W", "Santa Rosa", CA
   35,   40,   48, "N",    105,   56,   59, "W", "Santa Fe", NM
   34,   25,   11, "N",    119,   41,   59, "W", "Santa Barbara", CA
   33,   45,   35, "N",    117,   52,   12, "W", "Santa Ana", CA
   37,   20,   24, "N",    121,   52,   47, "W", "San Jose", CA
   37,   46,   47, "N",    122,   25,   11, "W", "San Francisco", CA
   41,   27,    0, "N",     82,   42,   35, "W", "Sandusky", OH
   32,   42,   35, "N",    117,    9,    0, "W", "San Diego", CA
   34,    6,   36, "N",    117,   18,   35, "W", "San Bernardino", CA
   29,   25,   12, "N",     98,   30,    0, "W", "San Antonio", TX
   31,   27,   35, "N",    100,   26,   24, "W", "San Angelo", TX
   40,   45,   35, "N",    111,   52,   47, "W", "Salt Lake City", UT
   38,   22,   11, "N",     75,   35,   59, "W", "Salisbury", MD
   36,   40,   11, "N",    121,   39,    0, "W", "Salinas", CA
   38,   50,   24, "N",     97,   36,   36, "W", "Salina", KS
   38,   31,   47, "N",    106,    0,    0, "W", "Salida", CO
   44,   56,   23, "N",    123,    1,   47, "W", "Salem", OR
   44,   57,    0, "N",     93,    5,   59, "W", "Saint Paul", MN
   38,   37,   11, "N",     90,   11,   24, "W", "Saint Louis", MO
   39,   46,   12, "N",     94,   50,   23, "W", "Saint Joseph", MO
   42,    5,   59, "N",     86,   28,   48, "W", "Saint Joseph", MI
   44,   25,   11, "N",     72,    1,   11, "W", "Saint Johnsbury", VT
   45,   34,   11, "N",     94,   10,   11, "W", "Saint Cloud", MN
   29,   53,   23, "N",     81,   19,   11, "W", "Saint Augustine", FL
   43,   25,   48, "N",     83,   56,   24, "W", "Saginaw", MI
   38,   35,   24, "N",    121,   29,   23, "W", "Sacramento", CA
   43,   36,   36, "N",     72,   58,   12, "W", "Rutland", VT
   33,   24,    0, "N",    104,   31,   47, "W", "Roswell", NM
   35,   56,   23, "N",     77,   48,    0, "W", "Rocky Mount", NC
   41,   35,   24, "N",    109,   13,   48, "W", "Rock Springs", WY
   42,   16,   12, "N",     89,    5,   59, "W", "Rockford", IL
   43,    9,   35, "N",     77,   36,   36, "W", "Rochester", NY
   44,    1,   12, "N",     92,   27,   35, "W", "Rochester", MN
   37,   16,   12, "N",     79,   56,   24, "W", "Roanoke", VA
   37,   32,   24, "N",     77,   26,   59, "W", "Richmond", VA
   39,   49,   48, "N",     84,   53,   23, "W", "Richmond", IN
   38,   46,   12, "N",    112,    5,   23, "W", "Richfield", UT
   45,   38,   23, "N",     89,   25,   11, "W", "Rhinelander", WI
   39,   31,   12, "N",    119,   48,   35, "W", "Reno", NV
   50,   25,   11, "N",    104,   39,    0, "W", "Regina", SA
   40,   10,   48, "N",    122,   14,   23, "W", "Red Bluff", CA
   40,   19,   48, "N",     75,   55,   48, "W", "Reading", PA
   41,    9,   35, "N",     81,   14,   23, "W", "Ravenna", OH 
`;
}
