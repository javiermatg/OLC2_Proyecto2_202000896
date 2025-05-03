import React, { useState } from "react";
import { AppBar, Tabs, Tab, Box, Button, IconButton, Toolbar, Paper, Table, TableContainer, TableCell, TableBody, TableHead, TableRow, Typography, } from "@mui/material";
import CodeMirror from "@uiw/react-codemirror";
import { javascript } from "@codemirror/lang-javascript";
import { Save, FolderOpen, PlayArrow, ErrorOutline, TableChart,AccountTree } from "@mui/icons-material";
import './App.css';

const Editor = () => {
  const [tabs, setTabs] = useState([{ id: 1, name: "Archivo 1", content: "" }]);
  const [currentTab, setCurrentTab] = useState(0);
  const [output, setOutput] = useState([{ id: 1, name: "Archivo 1", content: "" }]);

  //Errors
  const [errors, setErrors] = useState([{ id: 1, name: "Archivo 1", content: "" }]);
  const [loading, setLoading] = useState(false);
  const [showTableErrors, setShowTableErrors] = useState(false);

  //Symbols
  const [symbols, setSymbols] = useState([{ id: 1, name: "Archivo 1", content: "" }]);
  const [loadingSymbols, setLoadingSymbols] = useState(false);
  const [showTableSymbols, setShowTableSymbols] = useState(false);



  const ShowErrors = () => {
    setShowTableErrors(!showTableErrors);
  };

  const ShowSymbols = () => {
    setShowTableSymbols(!showTableSymbols);
  }
  

  const handleChange = (event, newValue) => {
    setCurrentTab(newValue);
  };

  const handleAddTab = () => {
    const newId = tabs.length + 1;
    setTabs([...tabs, { id: newId, name: `Archivo ${newId}`, content: "" }]);
    setOutput([...output, { id: newId, name: `Archivo ${newId}`, content: "" }]);
    setCurrentTab(tabs.length);
  };

  const handleEditContent = (value) => {
    const newTabs = [...tabs];
    newTabs[currentTab].content = value;
    setTabs(newTabs);
  };

  const handleSaveFile = () => {
    const file = new Blob([tabs[currentTab].content], { type: "text/plain" });
    const url = URL.createObjectURL(file);
    const a = document.createElement("a");
    a.href = url;
    a.download = `${tabs[currentTab].name}.glt`;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  };

  const handleOpenFile = (event) => {
    const file = event.target.files[0];
    if (file && file.name.endsWith(".glt")) {
      const reader = new FileReader();
      reader.onload = (e) => {
        setTabs([...tabs, { id: tabs.length + 1, name: file.name, content: e.target.result }]);
        setCurrentTab(tabs.length);
      };
      reader.readAsText(file);
    }
  };


  //Fetch para ejecutar el código
  const fetchExecute = async () => {
    setLoading(true);
    setLoadingSymbols(true);
    console.log(tabs[currentTab].content);
    try{
      const response = await fetch('http://localhost:5293/compile', {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ code: tabs[currentTab].content }),
      });

      const data = await response.json();
      console.log("EEEh")
      console.log(data);
      //output[currentTab].content = (data);
      const newOutput = [...output];
      newOutput[currentTab].content = data.result.toString();
      setOutput(newOutput);

      //GetErrrors
      const newErros = [...errors];
      newErros[currentTab].content = data.errors;
      setErrors(newErros);
      //console.log(output[currentTab]);
      //console.log(output[currentTab].content);

      const newSymbols = [...symbols];
      newSymbols[currentTab].content = data.symbols;
      setSymbols(newSymbols);

      console.log("HERE IS IN FRON SYMBOLS")
      console.log(data.symbols)
      console.log("HERE IS IN FRON SYMBOLS")
      
      
    } catch (error) {
      console.log("Error en la ejecución");
      
      
      
      

    } finally {
      setLoading(false);  
      setLoadingSymbols(false);
    }
  };

  

  // fetch to watch errors
  /*
  const fetchErrors = async () => {
    try {
      const response = await fetch('http://localhost:5293/compile/errors');
      const data = await response.json();
      console.log(data);
    } catch (error) {
      console.log(error);
    } finally {
      console.log('Errors fetched');
    }

  }; */




  return (
    
    <Box sx={{ display: "flex", flexDirection: "column", height: "100vh", width: "100vw" }}>
      <AppBar position="static" sx={{ backgroundColor: "#1976d2", padding: "5px" }}>
        <Toolbar>
          <Button color="inherit" onClick={handleAddTab}>Nuevo Archivo</Button>
          <input
            type="file"
            accept=".glt"
            style={{ display: "none" }}
            id="fileInput"
            onChange={handleOpenFile}
          />
          <label htmlFor="fileInput">
            <IconButton color="inherit" component="span">
              <FolderOpen />
            </IconButton>
          </label>
          <IconButton color="inherit" onClick={handleSaveFile}>
            <Save />
          </IconButton>
        </Toolbar>
      </AppBar>
      <Tabs color="black" value={currentTab} onChange={handleChange} variant="scrollable">
        {tabs.map((tab, index) => (
          <Tab key={tab.id} label={tab.name} />
        ))}
      </Tabs>
      <Box sx={{ flexGrow: 1, padding: "10px" }}>
        <CodeMirror
          value={tabs[currentTab].content}
          //extensions={[javascript()]}
          height="100%"
          theme="dark"
          onChange={handleEditContent}
        />
      </Box>
      <Box sx={{ display: "flex", justifyContent: "space-between", padding: "20px"}}>
        <Button variant="contained" color="success" startIcon={<PlayArrow />} onClick={fetchExecute}>Ejecutar </Button>
        <Button onClick={ShowErrors} disabled={loading}variant="contained" color="error" startIcon={<ErrorOutline />} >Errores</Button>
        <Button onClick={ShowSymbols} disabled={loadingSymbols}variant="contained" color="primary" startIcon={<TableChart />}>Tabla Símbolos</Button>
        <Button variant="contained" color="secondary" startIcon={<AccountTree />}>AST</Button>
      </Box>
        <label >Consola:
            
        </label>
      <Box sx={{ flexGrow: 1, padding: "10px" }}>
        <CodeMirror
          value={output[currentTab].content}
          readOnly="true"
          height="100%"
          theme="dark"
          
        />
      </Box>
      


      {showTableErrors && (
      <div className="mt-6 w-full max-w-4xl">
        <AppBar position="static" color='error' sx={{ color: 'warning' }}>
          <Toolbar>
              <Typography variant="h4" sx={{ flexGrow: 1, textAlign: 'center' }}>
                   Tabla Errores
              </Typography>
           </Toolbar>
        </AppBar>
        <TableContainer component={Paper} elevation={3} sx={{ borderRadius: "10px", overflow: "hidden" }}>
          <Table>
            <TableHead>
              <TableRow sx={{ backgroundColor: "#1976D2" }}>
              <TableCell sx={{ color: "white", fontWeight: "bold" }}>Tipo de Error</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Descripción</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Linea</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Columna</TableCell>
                
              </TableRow>
            </TableHead>
            <TableBody>
              {errors[currentTab].content.map((error, index) => (
                <TableRow key={index} hover>
                  <TableCell>{error.errorType}</TableCell>
                  <TableCell>{error.description}</TableCell>
                  <TableCell>{error.line}</TableCell>
                  <TableCell>{error.column}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div> )}

      {showTableSymbols && (
      <div className="mt-6 w-full max-w-4xl">
        <AppBar position="static" color='error' sx={{ color: 'secondary' }}>
          <Toolbar>
              <Typography variant="h4" sx={{ flexGrow: 1, textAlign: 'center' }}>
                   Tabla Simbolos
              </Typography>
           </Toolbar>
        </AppBar>
        <TableContainer component={Paper} elevation={3} sx={{ borderRadius: "10px", overflow: "hidden" }}>
          <Table>
            <TableHead>
              <TableRow sx={{ backgroundColor: "#1976D2" }}>
              <TableCell sx={{ color: "white", fontWeight: "bold" }}>ID</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Tipo Símbolo</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Tipo Dato</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Ámbito</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Línea</TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>Columna</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>

              
              

              {symbols[currentTab].content.map((item, index) => (
                  <TableRow key={index} hover>
                    
                    <TableCell>{item.id}</TableCell>
                    <TableCell>{item.typeSymbol}</TableCell>
                    <TableCell>{item.typeData}</TableCell>
                    <TableCell>{item.nameEnv}</TableCell>
                    <TableCell>{item.line}</TableCell>
                    <TableCell>{item.column}</TableCell>
                  </TableRow>
              ))}
              
              
            </TableBody>
          </Table>
        </TableContainer>
      </div> )}
    </Box>

    
  );
};

export default Editor;