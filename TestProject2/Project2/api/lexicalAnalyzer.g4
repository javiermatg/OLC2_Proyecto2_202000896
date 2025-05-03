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



init: lstinstructions ;

lstinstructions: instruction (instruction)* ;

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


;

//funcDcl: 'function' ID '('PARAMS?')': ID '{'dcl*'}'
//params: param (',' param)*;
//param: ID ':' ID;
funcInstructions: 'func' ID '(' funcParams? ')' ':' returnT=type? '{' lstinstructions '}' #FuncStmt;
funcParams: ID type (',' ID type)* ;
//funExecute: expr func  #FunctionCall;

func: '(' pars? ')' ;
pars: expr (',' expr)* ;

stmtSlice: 'var' ID '[]' type ('=' '{' (expr|',')* '}')? #StmtSlices ;
stmtVariables: 'var' ID type ('=' expr)? #StmtVar ;
stmtAssign: ID ':=' expr #StmtVarAssign;
//assign: ID ('='|':='|'++'|'--'|'+='| '-=') expr? #AssingVar;
print: 'fmt.Println' '('(expr|',')+')'  #PrintVar;
instructionIf: 'if' exprIf=expr '{' lsIf=lstinstructions '}' (('else if' exprElseIf=expr'{' lsElseIf=lstinstructions'}'))* ('else' '{' lsElse=lstinstructions '}')? #IfStmt;

forInstruction: 'for' ('(' stmtAssign ';')? expr (';' forDeclare ')')? '{' lsfor=lstinstructions '}' #ForStmt;
forDeclare: /*assign |*/  expr;

switchInstruction: 'switch' expr '{' cases ('default' ':' lsDefautl=lstinstructions)? '}' #SwitchStmt;
cases: ('case' expr ':' lstinstructions)+;

breakInstruction: 'break' #breakTransfer;  
continueInstruction: 'continue' #continueTransfer;
returnFunc:'return' expr?  #ReturnStmt;

expression: expr #StmtExpr;

//funcInstructions: 'func' ID '(' funcParams? ')' ':' returnT=type '{' lstinstructions '}' #FuncStmt;
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

