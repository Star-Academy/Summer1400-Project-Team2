import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
declare var require: any;

@Injectable({
  providedIn: 'root'
})
export class OgmaService {
  constructor(private http: HttpClient) {}
  public ogma: any;
  public initConfig(configuration = {}) {
    const Ogma = require('../../../../assets/ogma.min.js');
    this.ogma = new Ogma(configuration);
    this.addEdgeRule();
    this.addNodeRule();
    this.setHoverStyleNodes();
    this.setHoverStyleEdges();
    this.ogma.view.locateGraph();
  }
  public loadGraph(){
    this.loadPipeline(0).subscribe(res => {
      console.log(res);
      this.ogma.setGraph(res);
      this.ogma.view.locateGraph();
      this.setGrid();
    });
  }
  public async addNode(type: string) {
    const id = this.getNodeslength();
    this.ogma.addNode({
      id: this.getNodeslength(),
      data: { type: type },
      attributes: {
        shape: 'square',
        text: {
          content: type
        }
      }
    });
    this.setGrid();
    this.ogma.addNode({
      id: this.getNodeslength(),
      data: {
        type: 'plus'
      }
    });
    this.setGrid();
    const ogmaJson = await this.exportGraph();    
    console.log(ogmaJson);
    this.sendPipeline(0,ogmaJson);
    return id;
  }
  public async addLink(idSrc: number, idTarget: number) {
    this.ogma.addEdge({
      id: idSrc + ',' + (idSrc + 1),
      source: idSrc,
      target: idSrc + 1
    });
    this.ogma.addEdge({
      id: idSrc + 1 + ',' + idTarget,
      source: idSrc + 1,
      target: idTarget
    });
    this.setGrid();
    const ogmaJson = await this.exportGraph();
    console.log(ogmaJson);
    this.sendPipeline(0,ogmaJson);
  }

  public addEdgeRule() {
    this.ogma.styles.addEdgeRule({
      width: 7,
      color: 'grey'
    });
  }
  public addNodeRule() {
    this.ogma.styles.addRule({
      nodeAttributes: {
        color: function (node: any) {
          if (node.getAttribute('shape') === 'square') {
            return '#326C99';
          } else {
            return 'transparent';
          }
        },
        radius: function (node: any) {
          if (node.getAttribute('shape') === 'square') {
            return 50;
          } else {
            return 20;
          }
        },
        image: {
          url: function (node: any) {
            if (node.getAttribute('shape') === 'circle') {
              return '../../../../assets/images/icons/plus-button.png';
            } else {
              return;
            }
          },
          scale: 1
        },
        badges: {
          bottomRight: {
            image: {
              url: function (node: any) {
                if (node.getData('type') == 'join') {
                  return '../../../../assets/images/icons/join-button.svg';
                } else if (node.getData('type') == 'filter') {
                  return '../../../../assets/images/icons/filter-button.svg';
                } else if (node.getData('type') == 'aggregate') {
                  return '../../../../assets/images/icons/aggregate-button.svg';
                }
                return;
              },
              scale: 0.7
            }
          }
        },
        text: {
          position: 'center',
          color: 'white',
          size: 17
        }
      }
    });
  }

  public getNodeslength() {
    return this.ogma.getNodes().size;
  }

  public getEdgesLength() {
    return this.ogma.getEdges().size;
  }

  public setGrid() {
    this.ogma.layouts.sequential({
      direction: 'LR',
      duration: 300,
      nodeDistance: 200,
      levelDistance: 200,
      locate: true
    });
    this.ogma.view.locateGraph();
  }
  public async createFirstNode(pipelineId: number) {
    this.addNode('source');
    this.ogma.addNode({
      id: this.getNodeslength(),
      data: { type: 'destination' },
      attributes: {
        shape: 'square',
        text: {
          content: 'destination'
        }
      }
    });
    this.addLink(0, 2);
    this.setGrid();
    const ogmaJson = await this.exportGraph();    
    this.sendPipeline(pipelineId,ogmaJson);
  }

  public getSelectedEdge(nodeId: string) {
    const selectedEdgeObj = this.ogma
      .getNode(nodeId)
      .getAdjacentEdges()
      .filter((edge: any) => {
        return this.getEdgeSource(edge) == nodeId;
      });
    return this.ogma.getEdge(selectedEdgeObj.getId()[0]);
  }

  public async onPluseNode(nodeId: string, processorType: string) {
    const selectedEdge = this.getSelectedEdge(nodeId);
    const selectedEdge_target = this.getEdgeTarget(selectedEdge);
    const addedNodeId = await this.addNode(processorType);
    this.setEdgeTarget(selectedEdge, addedNodeId);
    this.addLink(addedNodeId, selectedEdge_target);
    this.setGrid();
  }

  public clickOnEdge(edge: any) {
    console.log('clicked on an edge between ', this.getEdgeSource(edge), ' and', this.getEdgeTarget(edge));
  }
  public setHoverStyleNodes() {
    const stylesObj = {
      text: {
        color: 'black',
        backgroundColor: 'transparent'
      },
      outline: {
        enabled: false
      },
      outerStroke: { color: 'rgb(75, 75, 75)' }
    };
    this.ogma.styles.setHoveredNodeAttributes(stylesObj);
    this.ogma.styles.setSelectedNodeAttributes(stylesObj);
  }

  public setHoverStyleEdges() {
    const stylesObj = {
      color: 'rgb(75, 75, 75)',
      text: {
        color: 'black',
        backgroundColor: 'transparent'
      },
      outline: {
        enabled: false
      }
    };
    this.ogma.styles.setHoveredEdgeAttributes(stylesObj);
    this.ogma.styles.setSelectedEdgeAttributes(stylesObj);
  }

  public getEdgeTarget(edge: any) {
    return edge.getTarget().getId();
  }

  public getEdgeSource(edge: any) {
    return edge.getSource().getId();
  }
  public setEdgeSource(edge: any, sourceId: string) {
    edge.setSource(sourceId);
  }
  public setEdgeTarget(edge: any, targetId: string) {
    edge.setTarget(targetId);
  }
  public setZoomIn() {
    let number = Math.floor(this.ogma.view.getZoom()) + 1;
    this.ogma.view.setZoom(number).then((res: any) => {});
  }

  public setZoomOut() {
    this.ogma.view.zoomOut().then((res: any) => {});
  }
  public  deleteNode = async() => {
    const selectedNodes = this.ogma.getSelectedNodes();
    if (selectedNodes) {
      const firstNode = selectedNodes.get(0);
      if (firstNode.getAttribute('shape') !== 'square') {
        return;
      }
      const selectedEdge = this.getSelectedEdge(firstNode.getId());
      const adjacentNodes1 = firstNode.getAdjacentNodes();
      let adjacentNodes2;
      let secondNode: any;
      let thirdNodeId: any;
      let beginNodeId: any;
      let secondNode_id = this.getEdgeTarget(selectedEdge);
      secondNode = this.ogma.getNode(secondNode_id);
      adjacentNodes1.forEach((node: any) => {
        if (node !== secondNode) {
          beginNodeId = node.getId();
        }
      });
      if (secondNode) {
        adjacentNodes2 = secondNode.getAdjacentNodes();
        adjacentNodes2.forEach((node: any) => {
          if (node !== firstNode) {
            thirdNodeId = node.getId();
          }
        });
      }
      this.ogma.removeNode(firstNode);
      this.ogma.removeNode(secondNode);
      this.ogma.addEdge({
        id: beginNodeId + ',' + thirdNodeId,
        source: beginNodeId,
        target: thirdNodeId
      });
    }
    this.setGrid();
    const ogmaJson = await this.exportGraph();    
    this.sendPipeline(0,ogmaJson);
  };

  async exportGraph() {
    const res = await this.ogma.export
      .json({
        download: false,
        pretty: true,
        nodeAttributes: ['x', 'y', 'shape', 'text'],
        edgeAttributes: []
      })
      .then((res: any) => res);
    return res;
  }
  sendPipeline(id: number, pipelineBody: any) {    
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':'application/json'        
      })
    };

    this.http
      .put('https://codestar.iran.liara.run/pipeline/' + id,pipelineBody,httpOptions)
      .subscribe(response => {
        console.log(response);
      });
  }

  loadPipeline(id: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':'application/json'        
      })
    };
    return this.http.get('https://codestar.iran.liara.run/pipeline/' + id,httpOptions);
  }
}
