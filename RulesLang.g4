grammar RulesLang;

// Tokens (Análise Léxica)
ID: [a-zA-Z_][a-zA-Z_0-9]* ;
INT: [0-9]+ ;
STRING: '"' ( ~["\\] | '\\' . )* '"' ;
FUNCTION: 'Upper' | 'Lower' | 'Length' | ; // Adicionarei outras funções posteriormente
WS: [ \t\r\n]+ -> skip ; // Ignora espaços em branco

// Gramática (Análise Sintática)
ruleset: rule+ ;
rule: 'rule' ID '{' expression '}' ;
expression: functionCall | arithmeticExpression | STRING | INT | ID ;
functionCall: FUNCTION '(' expression (',' expression)* ')' ;
arithmeticExpression: term ( ( '+' | '-' ) term )* ;
term: factor ( ( '*' | '/' ) factor )* ;
factor: INT | ID | '(' expression ')' ;
