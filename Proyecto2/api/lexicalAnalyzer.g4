grammar lexicalAnalyzer;

LINE_COMMENT : '//' ~[\r\n]* -> channel(HIDDEN) ; 
MULTILINE_COMMENT : '/*' .*? '*/' -> channel(HIDDEN) ; 

NEWLINE : [ \r\n\t]+ -> channel(HIDDEN);
NULL: 'nil' ;
BOOL : ('true'|'false') ;
ID: ('_')?[a-zA-Z]+('_')?([0-9]+)? ;
INT     : [0-9]+ ;
DECIMAL : [0-9]+('.'[0-9]+) ;
CHARACTER : ('"'|'\'') [.]? ('"'|'\'') ;
STRING: ('"'|'\'') (~["\r\n] | '""')* ('"'|'\'') ;
//STRING: '"' ~'"'* '"';



init: instruction* ;



instruction: expression (';')?
| stmtVariables (';')?
| stmtAssign (';')?
| funcInstructions (';')?
//| funExecute (';')?
//| assign (';')?
| print (';')?
| instructionIf (';')?
| stmtSlice (';')?
//| funcInstructions (';')?
//| funcExecute (';')?
//| returnFunc (';')?

| forInstruction (';')?
| switchInstruction (';')?
| breakInstruction (';')?
| continueInstruction (';')?
| returnFunc (';')?
| block (';')?

;

block: '{' instruction* '}'  #BlockStmt;
funcInstructions: 'func' ID '(' funcParams? ')' ':' returnT=type? '{' instruction '}' #FuncStmt;
funcParams: ID type (',' ID type)* ;
//funExecute: expr func  #FunctionCall;

func: '(' pars? ')' ;
pars: expr (',' expr)* ;

stmtSlice: 'var' ID '[]' type ('=' '{' (expr|',')* '}')? #StmtSlices ;
stmtVariables: 'var' ID type ('=' expr)?  #StmtVar ;
stmtAssign: ID ':=' expr #StmtVarAssign;
//assign: ID ('='|':='|'++'|'--'|'+='| '-=') expr? #AssingVar;
print: 'fmt.Println' '('expr (',' expr)*')'  #PrintVar;
instructionIf: 'if' exprIf=expr '{' lsIf=instruction '}' (('else if' exprElseIf=expr'{' lsElseIf=instruction'}'))* ('else' '{' lsElse=instruction '}')? #IfStmt;

forInstruction: 'for' ('(' forDeclare ';')? expr (';' forDeclare ')')? '{' lsfor=instruction '}' #ForStmt;
forDeclare: /*assign |*/  expr;

switchInstruction: 'switch' expr '{' cases ('default' ':' lsDefautl=instruction)? '}' #SwitchStmt;
cases: ('case' expr ':' instruction)+;

breakInstruction: 'break' #breakTransfer;  
continueInstruction: 'continue' #continueTransfer;
returnFunc:'return' expr?  #ReturnStmt;

expression: expr #StmtExpr;

//funcInstructions: 'func' ID '(' funcParams? ')' ':' returnT=type '{' instruction '}' #FuncStmt;
//funcExecute: ID '(' arguments? ')' ;
//funcParams: ID type (',' ID type)* ; 
//arguments: expr (',' expr)* ;




type: 'int'
| 'float64'
| 'string'
| 'bool'
| 'rune'
;

//func: '(' pars? ')' ;
//pars: expr (',' expr)* ;




expr: '-' expr            #Negate
| expr func+            #FunctionCall


//| expr ('++')  #IncrementStmt
//| expr ('--')  #DecrementStmt
| expr ('*'|'/') expr #MulDiv
| expr ('+'|'-') expr #AddSub
| expr '%' expr        #Module

| '!' right=expr                     #NegateOperator

| 'strconv.Atoi' '(' expr ')' #ConvertInt
| 'strconv.ParseFloat' '(' expr ')' #ConvertFloat
| 'reflect.TypeOf' '(' expr ')' #TypeOf
| left=expr operador='==' right=expr #RelationalOperator
| left=expr operador='!=' right=expr #RelationalOperator
| left=expr operador='>=' right=expr #RelationalOperator
| left=expr operador='>' right=expr  #RelationalOperator
| left=expr operador='<=' right=expr #RelationalOperator
| left=expr operador='<' right=expr  #RelationalOperator
| left=expr operador='&&' right=expr #LogicOperator
| left=expr operador='||' right=expr #LogicOperator
| ID ('='|'++'|'--'|'+='| '-=') expr? #AssingVar
| BOOL                  #Boolean
| NULL                  #Null 
| ID                    #Identifier
| INT                   #Number
| DECIMAL               #Decimal
| CHARACTER             #Character
| STRING                #String
//| funcExecute #FunctionExecute
|'(' expr ')'           #Parens
;

