using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidltourplaner.Model
{
    internal class MainModel
    {
        // Liste von allen angelegten Knoten
        public List<Vertex> m_allVertices { get; private set; }

        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public MainModel()
        {
            m_allVertices = new List<Vertex>();
        }

        /// <summary>
        /// Standarddaten von Lokalen in Salzburg und Verknüpfungen zu den Nachbarlokalen werden angelegt. 
        /// </summary>
        public void DefaultData()
        {
            // Anlegen der Knoten
            Vertex monkeys = new Vertex(47.80064507790365, 13.048103825465349, "Monkey cafe.bar");
            Vertex segabar = new Vertex(47.80004719908009, 13.046236505471862, "Segabar");
            Vertex steinlechners = new Vertex(47.798889396189196, 13.063847967838894, "Steinlechners");
            Vertex augustinerbraeu = new Vertex(47.80568, 13.03377, "Augustiner Bräu Kloster Mülln");
            Vertex watzmann = new Vertex(47.80101, 13.04686, "Watzmann Cultbar");
            Vertex partymaus = new Vertex(47.79197, 12.987, "Partymaus");
            Vertex schnaitlpub = new Vertex(47.80299, 13.04572, "Schnaitl Pub");
            Vertex rockhouse = new Vertex(47.80705, 13.05895, "Rockhouse Salzburg");
            Vertex weisse = new Vertex(47.80689, 13.05172, "Die Weisse");
            Vertex stiegelkeller = new Vertex(47.79645, 13.04801, "Stiegelkeller");
            Vertex urbankeller = new Vertex(47.80722, 13.06083, "Urbankeller Salzburg");
            Vertex cityalm = new Vertex(47.79979, 13.04678, "City Alm");
            Vertex omalleys = new Vertex(47.80012, 13.04607, "O'Malley's Irish Pub");
            Vertex flip = new Vertex(47.80114, 13.0389, "Flip");
            Vertex stiegelbrauwelt = new Vertex(47.79368, 13.02143, "Stiegl-Brauwelt");
            Vertex citybeats = new Vertex(47.80069, 13.04127, "City Beats");


            // Nachbarknoten
            // Monkeys
            monkeys.AddNeighborVertex(segabar);
            monkeys.AddNeighborVertex(steinlechners);
            monkeys.AddNeighborVertex(watzmann);
            monkeys.AddNeighborVertex(omalleys);
            monkeys.AddNeighborVertex(cityalm);
            monkeys.AddNeighborVertex(citybeats);

            // Segabar
            segabar.AddNeighborVertex(monkeys);
            segabar.AddNeighborVertex(watzmann);

            // Steinlechner
            steinlechners.AddNeighborVertex(urbankeller);
            steinlechners.AddNeighborVertex(monkeys);
            steinlechners.AddNeighborVertex(cityalm);
            steinlechners.AddNeighborVertex(stiegelkeller);

            // Augustiner Bräu
            augustinerbraeu.AddNeighborVertex(flip);
            augustinerbraeu.AddNeighborVertex(stiegelbrauwelt);
            augustinerbraeu.AddNeighborVertex(partymaus);
            augustinerbraeu.AddNeighborVertex(weisse);
            augustinerbraeu.AddNeighborVertex(schnaitlpub);

            // Watzmann
            watzmann.AddNeighborVertex(segabar);
            watzmann.AddNeighborVertex(monkeys);
            watzmann.AddNeighborVertex(omalleys);
            watzmann.AddNeighborVertex(schnaitlpub);
            watzmann.AddNeighborVertex(citybeats);

            // Partymaus
            partymaus.AddNeighborVertex(stiegelbrauwelt);
            partymaus.AddNeighborVertex(augustinerbraeu);
            partymaus.AddNeighborVertex(flip);

            // Schnaitl Pub
            schnaitlpub.AddNeighborVertex(weisse);
            schnaitlpub.AddNeighborVertex(rockhouse);
            schnaitlpub.AddNeighborVertex(watzmann);
            schnaitlpub.AddNeighborVertex(omalleys);
            schnaitlpub.AddNeighborVertex(citybeats);
            schnaitlpub.AddNeighborVertex(augustinerbraeu);

            // Rockhouse
            rockhouse.AddNeighborVertex(weisse);
            rockhouse.AddNeighborVertex(schnaitlpub);
            rockhouse.AddNeighborVertex(urbankeller);

            // Weisse
            weisse.AddNeighborVertex(rockhouse);
            weisse.AddNeighborVertex(schnaitlpub);
            weisse.AddNeighborVertex(augustinerbraeu);

            // Stieglkeller
            stiegelkeller.AddNeighborVertex(stiegelbrauwelt);
            stiegelkeller.AddNeighborVertex(steinlechners);
            stiegelkeller.AddNeighborVertex(cityalm);
            stiegelkeller.AddNeighborVertex(flip);
            stiegelkeller.AddNeighborVertex(citybeats);

            // Urbankeller
            urbankeller.AddNeighborVertex(rockhouse);
            urbankeller.AddNeighborVertex(steinlechners);

            // Cityalm
            cityalm.AddNeighborVertex(omalleys);
            cityalm.AddNeighborVertex(steinlechners);
            cityalm.AddNeighborVertex(stiegelkeller);
            cityalm.AddNeighborVertex(monkeys);

            // O'Mally's
            omalleys.AddNeighborVertex(watzmann);
            omalleys.AddNeighborVertex(monkeys);
            omalleys.AddNeighborVertex(cityalm);
            omalleys.AddNeighborVertex(citybeats);
            omalleys.AddNeighborVertex(schnaitlpub);

            // Flip
            flip.AddNeighborVertex(augustinerbraeu);
            flip.AddNeighborVertex(stiegelbrauwelt);
            flip.AddNeighborVertex(stiegelkeller);
            flip.AddNeighborVertex(citybeats);
            flip.AddNeighborVertex(partymaus);

            // Stiegl-Brauwelt
            stiegelbrauwelt.AddNeighborVertex(partymaus);
            stiegelbrauwelt.AddNeighborVertex(augustinerbraeu);
            stiegelbrauwelt.AddNeighborVertex(flip);
            stiegelbrauwelt.AddNeighborVertex(stiegelkeller);

            // City Beats
            citybeats.AddNeighborVertex(flip);
            citybeats.AddNeighborVertex(omalleys);
            citybeats.AddNeighborVertex(schnaitlpub);
            citybeats.AddNeighborVertex(watzmann);
            citybeats.AddNeighborVertex(monkeys);
            citybeats.AddNeighborVertex(stiegelkeller);

            // Liste aller Knoten generieren
            m_allVertices.Add(monkeys);
            m_allVertices.Add(segabar);
            m_allVertices.Add(steinlechners);
            m_allVertices.Add(augustinerbraeu);
            m_allVertices.Add(watzmann);
            m_allVertices.Add(partymaus);
            m_allVertices.Add(schnaitlpub);
            m_allVertices.Add(rockhouse);
            m_allVertices.Add(weisse);
            m_allVertices.Add(urbankeller);
            m_allVertices.Add(stiegelkeller);
            m_allVertices.Add(cityalm);
            m_allVertices.Add(omalleys);
            m_allVertices.Add(flip);
            m_allVertices.Add(stiegelbrauwelt);
            m_allVertices.Add(citybeats);
        }

    }
}
