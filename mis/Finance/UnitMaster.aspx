<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UnitMaster.aspx.cs" Inherits="mis_Finance_UnitMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Unit Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Unit</label>
                                <asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Quantity Type</label>
                                <asp:DropDownList ID="TextBox1" runat="server" placeholder="Unit" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>UQC Code</label>
                                <asp:TextBox ID="TextBox2" runat="server" placeholder="Unit" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" Text="Accept" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-block btn-default" Text="Clear" />
                            </div>
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                </div>
                <div class="box-body">

                    <table class="table table-bordered table-striped table-hover">
                        <thead>
                            <tr class="row-1 odd">
                                <th class="column-1">S.No.</th>
                                <th class="column-2">Unit</th>
                                <th class="column-3">Quantity Type	</th>
                                <th class="column-4">UQC Code</th>
                            </tr>
                        </thead>
                        <tbody class="row-hover">
                            <tr class="row-2 even">
                                <td class="column-1">1	</td>
                                <td class="column-2">BAGS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BAG</td>
                            </tr>
                            <tr class="row-3 odd">
                                <td class="column-1">2	</td>
                                <td class="column-2">BALE	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BAL</td>
                            </tr>
                            <tr class="row-4 even">
                                <td class="column-1">3	</td>
                                <td class="column-2">BUNDLES	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BDL</td>
                            </tr>
                            <tr class="row-5 odd">
                                <td class="column-1">4		</td>
                                <td class="column-2">BUCKLES</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BKL</td>
                            </tr>
                            <tr class="row-6 even">
                                <td class="column-1">5	</td>
                                <td class="column-2">BILLIONS OF UNITS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BOU</td>
                            </tr>
                            <tr class="row-7 odd">
                                <td class="column-1">6	</td>
                                <td class="column-2">BOX	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BOX	</td>
                            </tr>
                            <tr class="row-8 even">
                                <td class="column-1">7	</td>
                                <td class="column-2">BOTTLES	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BTL</td>
                            </tr>
                            <tr class="row-9 odd">
                                <td class="column-1">8	</td>
                                <td class="column-2">BUNCHES	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">BUN</td>
                            </tr>
                            <tr class="row-10 even">
                                <td class="column-1">9		</td>
                                <td class="column-2">CANS	</td>
                                <td class="column-3">Measure</td>
                                <td class="column-4">CAN</td>
                            </tr>
                            <tr class="row-11 odd">
                                <td class="column-1">10	</td>
                                <td class="column-2">CUBIC METER	</td>
                                <td class="column-3">Volume	</td>
                                <td class="column-4">CBM</td>
                            </tr>
                            <tr class="row-12 even">
                                <td class="column-1">11		</td>
                                <td class="column-2">CUBIC CENTIMETER</td>
                                <td class="column-3">Volume	</td>
                                <td class="column-4">CCM</td>
                            </tr>
                            <tr class="row-13 odd">
                                <td class="column-1">12	</td>
                                <td class="column-2">CENTIMETER	</td>
                                <td class="column-3">Length	</td>
                                <td class="column-4">CMS</td>
                            </tr>
                            <tr class="row-14 even">
                                <td class="column-1">13	</td>
                                <td class="column-2">CARTONS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">CTN</td>
                            </tr>
                            <tr class="row-15 odd">
                                <td class="column-1">14	</td>
                                <td class="column-2">DOZEN	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">DOZ</td>
                            </tr>
                            <tr class="row-16 even">
                                <td class="column-1">15	</td>
                                <td class="column-2">DRUM	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">DRM</td>
                            </tr>
                            <tr class="row-17 odd">
                                <td class="column-1">16	</td>
                                <td class="column-2">GREAT GROSS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">GGR</td>
                            </tr>
                            <tr class="row-18 even">
                                <td class="column-1">17	</td>
                                <td class="column-2">GRAMS	</td>
                                <td class="column-3">Weight	</td>
                                <td class="column-4">GMS</td>
                            </tr>
                            <tr class="row-19 odd">
                                <td class="column-1">18	</td>
                                <td class="column-2">GROSS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">GRS</td>
                            </tr>
                            <tr class="row-20 even">
                                <td class="column-1">19			</td>
                                <td class="column-2">GROSS YARDS</td>
                                <td class="column-3">Length</td>
                                <td class="column-4">GYD</td>
                            </tr>
                            <tr class="row-21 odd">
                                <td class="column-1">20	</td>
                                <td class="column-2">KILOGRAMS	</td>
                                <td class="column-3">Weight	</td>
                                <td class="column-4">KGS</td>
                            </tr>
                            <tr class="row-22 even">
                                <td class="column-1">21		</td>
                                <td class="column-2">KILOLITER	</td>
                                <td class="column-3">Volume</td>
                                <td class="column-4">KLR</td>
                            </tr>
                            <tr class="row-23 odd">
                                <td class="column-1">22	</td>
                                <td class="column-2">KILOMETRE	</td>
                                <td class="column-3">Length	</td>
                                <td class="column-4">KME</td>
                            </tr>
                            <tr class="row-24 even">
                                <td class="column-1">23	</td>
                                <td class="column-2">MILLILITRE	</td>
                                <td class="column-3">Volume	</td>
                                <td class="column-4">MLT</td>
                            </tr>
                            <tr class="row-25 odd">
                                <td class="column-1">24	</td>
                                <td class="column-2">METERS		</td>
                                <td class="column-3">Length</td>
                                <td class="column-4">MTR</td>
                            </tr>
                            <tr class="row-26 even">
                                <td class="column-1">25	</td>
                                <td class="column-2">NUMBERS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">NOS</td>
                            </tr>
                            <tr class="row-27 odd">
                                <td class="column-1">26	</td>
                                <td class="column-2">PACKS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">PAC</td>
                            </tr>
                            <tr class="row-28 even">
                                <td class="column-1">27	</td>
                                <td class="column-2">PIECES	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">PCS</td>
                            </tr>
                            <tr class="row-29 odd">
                                <td class="column-1">28		</td>
                                <td class="column-2">PAIRS</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">PRS</td>
                            </tr>
                            <tr class="row-30 even">
                                <td class="column-1">29	</td>
                                <td class="column-2">QUINTAL	</td>
                                <td class="column-3">Weight	</td>
                                <td class="column-4">QTL</td>
                            </tr>
                            <tr class="row-31 odd">
                                <td class="column-1">30		</td>
                                <td class="column-2">ROLLS</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">ROL</td>
                            </tr>
                            <tr class="row-32 even">
                                <td class="column-1">31	</td>
                                <td class="column-2">SETS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">SET</td>
                            </tr>
                            <tr class="row-33 odd">
                                <td class="column-1">32	</td>
                                <td class="column-2">SQUARE FEET	</td>
                                <td class="column-3">Area	</td>
                                <td class="column-4">SQF</td>
                            </tr>
                            <tr class="row-34 even">
                                <td class="column-1">33	</td>
                                <td class="column-2">SQUARE METERS	</td>
                                <td class="column-3">Area	</td>
                                <td class="column-4">SQM</td>
                            </tr>
                            <tr class="row-35 odd">
                                <td class="column-1">34			</td>
                                <td class="column-2">SQUARE YARDS</td>
                                <td class="column-3">Area</td>
                                <td class="column-4">SQY</td>
                            </tr>
                            <tr class="row-36 even">
                                <td class="column-1">35	</td>
                                <td class="column-2">TABLETS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">TBS</td>
                            </tr>
                            <tr class="row-37 odd">
                                <td class="column-1">36	</td>
                                <td class="column-2">TEN GROSS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">TGM</td>
                            </tr>
                            <tr class="row-38 even">
                                <td class="column-1">37	</td>
                                <td class="column-2">THOUSANDS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">THD</td>
                            </tr>
                            <tr class="row-39 odd">
                                <td class="column-1">38	</td>
                                <td class="column-2">TONNES	</td>
                                <td class="column-3">Weight	</td>
                                <td class="column-4">TON</td>
                            </tr>
                            <tr class="row-40 even">
                                <td class="column-1">39	</td>
                                <td class="column-2">TUBES	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">TUB</td>
                            </tr>
                            <tr class="row-41 odd">
                                <td class="column-1">40	</td>
                                <td class="column-2">US GALLONS	</td>
                                <td class="column-3">Volume	</td>
                                <td class="column-4">UGS</td>
                            </tr>
                            <tr class="row-42 even">
                                <td class="column-1">41	</td>
                                <td class="column-2">UNITS	</td>
                                <td class="column-3">Measure	</td>
                                <td class="column-4">UNT</td>
                            </tr>
                            <tr class="row-43 odd">
                                <td class="column-1">42	</td>
                                <td class="column-2">YARDS	</td>
                                <td class="column-3">Length	</td>
                                <td class="column-4">YDS</td>
                            </tr>
                            <tr class="row-44 even">
                                <td class="column-1">43	</td>
                                <td class="column-2">OTHERS		</td>
                                <td class="column-3"></td>
                                <td class="column-4">OTH</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

