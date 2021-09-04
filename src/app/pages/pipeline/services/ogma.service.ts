import { Injectable } from '@angular/core';
declare var require: any;

@Injectable({
  providedIn: 'root'
})
export class OgmaService {
  public ogma: any;
  textNode = 'center';
  zoomInSize = 3;
  public initConfig(configuration = {}) {
    const Ogma = require('../../../../assets/ogma.min.js');
    this.ogma = new Ogma(configuration);
    this.addEdgeRule();    
    this.setHoverStyleNodes();
    this.setHoverStyleEdges();
    this.ogma.view.locateGraph();
  }
  public addNode(type:string) {
    const id = this.getNodeslength();
    this.ogma.addNode({
      id: 'n' + this.getNodeslength(),
      attributes: {
        color: '#326C99',
        radius: 50,
        shape: 'square',
        text: {
          content: type,
          position: 'center',
          color: 'white',
          size: 17
        }
      }
    });
    this.setGrid();
    this.ogma.addNode({
      id: 'n' + this.getNodeslength(),
      attributes: {
        radius: 20,
        color: 'transparent',
        image: {
          url: '../../../../assets/images/icons/plus-button.png',
          scale: 1
        }
      }
    });
    this.setGrid();
    return id;
  }
  public addLink(idSrc: number, idTarget: number) {
    this.ogma.addEdge({
      id: 'e' + this.getEdgesLength(),
      source: 'n' + idSrc,
      target: 'n' + (idSrc + 1)
    });
    this.ogma.addEdge({
      id: 'e' + this.getEdgesLength(),
      source: 'n' + (idSrc + 1),
      target: 'n' + idTarget
    });
    this.setGrid();
  }

  public addEdgeRule() {
    this.ogma.styles.addEdgeRule({
      text: {
        content: (e: any) => 'Edge ' + e.getId()
      },
      width: 7
    });
  }
  public addNodeRule() {
    this.ogma.styles.addNodeRule({
      text: {
        content: (e: any) => 'Node ' + e.getId()
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
  public createFirstNode() {
    this.addNode('source');
    this.ogma.addNode({
      id: 'n' + this.getNodeslength(),
      attributes: {
        color: '#326C99',
        radius: 50,
        shape: 'square',
        text: {
          content: 'destination',
          position: 'center',
          size: 17,
          color: 'white'
        }
      }
    });
    this.addLink(0, 2);
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

  public onPluseNode(nodeId: string,processorType:string) {
    const selectedEdge = this.getSelectedEdge(nodeId);
    const selectedEdge_target = this.getEdgeTarget(selectedEdge);
    const selectedEdge_target_id = +selectedEdge_target.match(/(\d+)/)[0];
    const addedNodeId = this.addNode(processorType);
    this.setEdgeTarget(selectedEdge, 'n' + addedNodeId);
    this.addLink(addedNodeId, selectedEdge_target_id);
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
    this.ogma.view.setZoom(this.zoomInSize).then((res: any) => {
      this.zoomInSize++;
    });
  }

  public setZoomOut() {
    this.ogma.view.zoomOut().then((res: any) => {
      this.zoomInSize--;
    });
  }
}
