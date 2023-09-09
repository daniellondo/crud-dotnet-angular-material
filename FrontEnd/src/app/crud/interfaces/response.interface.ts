import { Branch } from "./branch.interface";
import { Currency } from "./currency.interface";

export interface EndpointBranchResponse {
  message: string;
  result:  Branch[];
  error:   null;
}

export interface EndpointCurrencyResponse {
  message: string;
  result:  Currency[];
  error:   null;
}
