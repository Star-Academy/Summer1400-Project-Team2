import { Injectable } from '@angular/core';
import { parse, ParseResult } from 'papaparse';

interface CsvParseOption {
  header?: boolean;
  delimiter?: string;
  newline?: string;
  escapeChar?: string;
  quoteChar?: string;
  comments?: string | boolean;
  preview?: number;
}

@Injectable()
export class CsvService {
  parse<T = unknown>(csv: string, options: CsvParseOption = {}): ParseResult<T> {
    return parse<T>(
      options.header ? csv : `${this.getdefaultheader(csv, options)}${csv}`,
      {
        ...options,
        header: true,
        skipEmptyLines: true,
        transformHeader: (header, i = 0) => header.trim() || `ستون ${i + 1}`
      }
    );
  }

  private getdefaultheader(csv: string, options: CsvParseOption = {}) {
    const { meta } = parse(csv, {
      ...options,
      preview: 1,
      header: true,
      skipEmptyLines: true
    });

    return `${meta.delimiter.repeat((meta.fields?.length || 2) - 1)}${meta.linebreak}`;
  }
}
